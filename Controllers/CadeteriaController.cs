using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_gonchyrobinson.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    public Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> _logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteriaSingleton("./CargaArchivos/Cadetes.json", "./CargaArchivos/Cadeterias.json");
    }

    [HttpGet(Name = "GetPedidos")]
    public List<Pedido> GetPedidos()
    {
        return cadeteria.GetListaPedidos();
    }
    [HttpGet("GetCadetes")]
    public List<Cadete> GetCadetes()
    {
        return cadeteria.GetListaCadetes();
    }
    [HttpGet("GetInforme")]
    public Informe GetInforme()
    {
        return cadeteria.GetInforme();
    }

    //EL Agregar pedido, debe ingresar el cadete en el body? (ver en PostMan el body de agregar pedido). En caso de que si, debe guardar el cadete en el json de cadetes?
    [HttpPost("AgregarPedido/Pedido={pedido}")]
    public ActionResult<string> AgregarPedido(Pedido pedido)
    {
        bool encuentra = cadeteria.CrearPedido(pedido);
        if (encuentra)
        {
            cadeteria.GuardarPedidos();
            cadeteria.GuardarCadetes();
            return Ok("Pedido agregado con exito");
        }
        else
        {
            return BadRequest("Error al agregar pedido");
        }
    }
    [HttpPut("AsignarPedido/IdPedido={idPedido}/idCadete={idCadete}")]
    public ActionResult<string> AsignarPedido(int idPedido, int idCadete)
    {
        bool agregado = cadeteria.AsignarCadeteAPedido(idCadete, idPedido);
        if (agregado)
        {
            cadeteria.GuardarPedidos();
            cadeteria.GuardarCadetes();
            return Ok("Se asigno correctamente el pedido al cadete");
        }
        else
        {
            return BadRequest("Error al asignar cadete al pedido");
        }
    }
    [HttpPut("CambiarEstadoPedido/idPedido={idPedido}/NuevoEstado={NuevoEstado}")]
    public ActionResult<string> CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {
        bool actualizado = cadeteria.ActualizarPedido(idPedido, NuevoEstado);
        if (actualizado)
        {
            cadeteria.GuardarPedidos();
            cadeteria.GuardarCadetes();
            return Ok("Estado cambiado correctamente");
        }
        else
        {
            return BadRequest("No se pudo actualizar el pedido");
        }
    }
    [HttpPut("CambiarCadetePedido/idPedido={idPedido}/idNuevoCadete={idNuevoCadete}")]
    public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        bool cambia = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
        if (cambia)
        {
            cadeteria.GuardarPedidos();
            cadeteria.GuardarCadetes();
            return Ok("Se modifico el cadete exitosamente");
        }
        else
        {
            return BadRequest("Error al actualizar pedido");
        }
    }

    [HttpGet]
    [Route("GetPedido/id={id}")]
    public ActionResult<Pedido> GetPedido(int id)
    {
        Pedido ped = cadeteria.GetPedido(id);
        if (ped.EsPredeterminado())
        {
            return BadRequest(ped);
        }
        else
        {
            return Ok(cadeteria.GetPedido(id));
        }
    }
    [HttpGet]
    [Route("GetCadete/id={id}")]
    public ActionResult<Cadete> GetCadete(int id)
    {
        Cadete encontrado = cadeteria.GetCadete(id);
        if(encontrado.EsPredeterminado()){
            return BadRequest(encontrado);
        }else
        {
            return Ok(encontrado);
        }
    }

    [HttpPost]
    [Route("AddCadete/{cad}")]
    public ActionResult<string> AddCadete(Cadete cad){
        cadeteria.AgregarCadete(cad);
        if(cadeteria.ExisteCadete(cad)){
            cadeteria.GuardarCadetes();
            return Ok("Cadete agregado con exito");
        }else
        {
            return BadRequest("Error al agregar cadete ");
        }
    }
}
