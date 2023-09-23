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
        cadeteria=Cadeteria.GetCadeteriaSingleton("./CargaArchivos/Cadetes.json", "./CargaArchivos/Cadeterias.json");
    }

    [HttpGet(Name = "GetPedidos")]
    public List<Pedido> GetPedidos(){
        return cadeteria.GetListaPedidos();
    }
    [HttpGet("GetCadetes")]
    public List<Cadete> GetCadetes(){
        return cadeteria.GetListaCadetes();
    }
    [HttpGet("GetInforme")]
    public Informe  GetInforme(){
        return cadeteria.GetInforme();
    }  
    [HttpPost("AgregarPedido/Pedido={pedido}")]
    public ActionResult<string> AgregarPedido(Pedido pedido){
        bool encuentra = cadeteria.CrearPedido(pedido);
        if(encuentra){
            cadeteria.GuardarPedidos();
            return Ok("Pedido agregado con exito");
        }else
        {
            return BadRequest("Error al agregar pedido");
        }
    }
    [HttpPut("AsignarPedido/IdPedido={idPedido}/idCadete={idCadete}")]
    public ActionResult<string> AsignarPedido(int idPedido, int idCadete){
        bool agregado = cadeteria.AsignarCadeteAPedido(idCadete,idPedido);
        if (agregado)
        {
            cadeteria.GuardarPedidos();
            return Ok("Se asigno correctamente el pedido al cadete");
        }else
        {
            return BadRequest("Error al asignar cadete al pedido");
        }
    }
    [HttpPut("CambiarEstadoPedido/idPedido={idPedido}/NuevoEstado={NuevoEstado}")]
    public ActionResult<string> CambiarEstadoPedido(int idPedido, int NuevoEstado){
        bool actualizado = cadeteria.ActualizarPedido(idPedido,NuevoEstado);
        if(actualizado){
            cadeteria.GuardarPedidos();
            return Ok("Estado cambiado correctamente");
        }else
        {
            return BadRequest("No se pudo actualizar el pedido");
        }
    }
    [HttpPut("CambiarCadetePedido/idPedido={idPedido}/idNuevoCadete={idNuevoCadete}")]
    public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete){
        bool cambia = cadeteria.ReasignarPedido(idPedido,idNuevoCadete);
        if (cambia)
        {
            cadeteria.GuardarPedidos();
            return Ok("Se modifico el cadete exitosamente");
        }else
        {
            return BadRequest("Error al actualizar pedido");
        }
    }
}
