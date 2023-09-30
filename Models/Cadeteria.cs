using System;
using System.Collections;
namespace tl2_tp4_2023_gonchyrobinson;
public class Cadeteria
{
    //Variables privadas
    private string nombre;
    private string direccion;
    private List<Cadete> cadetes;
    private AccesoADatosPedidos datosPedidos;
    private AccesoADatosCadetes datosCadetes;
    private List<Pedido> pedidos;


    //Variables publicas
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }




    //Constructores
    public Cadeteria()
    {

    }
    public Cadeteria(string nombre, string direccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.cadetes = new List<Cadete>();
        this.pedidos = new List<Pedido>();
    }
    public Cadeteria(string nombre, string direccion, AccesoADatosPedidos accesoPedidos)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.cadetes = new List<Cadete>();
        this.pedidos = new List<Pedido>();
        this.datosPedidos = accesoPedidos;
    }




    //Singleton
    private static Cadeteria cadeteriaSingleton;
    public static Cadeteria GetCadeteriaSingleton(string rutaCadetes, string rutaCadeterias)
    {
        if (cadeteriaSingleton == null)
        {
            AccesoADatosCadeteria helperCadeterias = new AccesoADatosCadeteria();
            cadeteriaSingleton = helperCadeterias.Obtener();
            cadeteriaSingleton.datosPedidos = new AccesoADatosPedidos();
            cadeteriaSingleton.datosCadetes = new AccesoADatosCadetes();
            cadeteriaSingleton.CargarCadetes();
            cadeteriaSingleton.CargarPedidos();
        }
        return cadeteriaSingleton;
    }

    //Lectura de archivos
    private void CargarCadetes()
    {
        this.cadetes = datosCadetes.Obtener();
    }
    private void CargarPedidos()
    {
        this.pedidos = this.datosPedidos.Obtener();
    }

    //Pedidos
    public void CrearPedido(string? nombreCliente, string? direccionCliente, string? telefonoCliente, string? datosReferenciaDireccionCliente, int idPedido, string? obsPedido, int idCadeteElegido)
    {
        Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
        Cadete asignado = this.cadetes.FirstOrDefault(cad => cad.Id == idCadeteElegido);
        Pedido pedido = new Pedido(idPedido, obsPedido, cliente, asignado);
        this.pedidos.Add(pedido);
    }
    public bool CrearPedido(Pedido pedido)
    {
        this.pedidos.Add(pedido);
        return pedidos.FirstOrDefault(ped => ped == pedido, null) != null;
    }
    public void CrearPedido(string? nombreCliente, string? direccionCliente, string? telefonoCliente, string? datosReferenciaDireccionCliente, int idPedido, string? obsPedido)
    {
        Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
        Pedido pedido = new Pedido(idPedido, obsPedido, cliente);
        this.pedidos.Add(pedido);
    }
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        Cadete cadete = this.cadetes.FirstOrDefault(cad => cad.Id == idCadete);
        if (cadete != null)
        {
            Pedido pedido = this.pedidos.FirstOrDefault(ped => ped.Numero == idPedido);
            if (pedido != null)
            {
                pedido.AsignarCadeteAPedido(cadete);
                return pedido.CoincideCadete(cadete);
            }
            else
            {
                Console.WriteLine("Pedido inexistente");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Id de cadete inexistente");
            return false;
        }
    }
    public bool ReasignarPedido(int pedidoId, int cadeteNuevoId)
    {
        Pedido pedidoEncontrado = this.pedidos.FirstOrDefault(ped => ped.Numero == pedidoId);
        Cadete cadeteNuevo = this.cadetes.FirstOrDefault(cad => cad.Id == cadeteNuevoId);
        if (cadeteNuevo != null && pedidoEncontrado != null)
        {
            pedidoEncontrado.AsignarCadeteAPedido(cadeteNuevo);
            return pedidoEncontrado.CoincideCadete(cadeteNuevo);
        }
        else
        {
            return false;
        }
    }
    public bool ActualizarPedido(int idElegido, int nuevoEstadoInt)
    {
        Pedido actualizar = this.pedidos.FirstOrDefault(ped => ped.Numero == idElegido, null);
        if (actualizar != null)
        {
            actualizar.ActualizarPedido(nuevoEstadoInt);
            return actualizar.Estado == Pedido.ConvierteIntAEnum(nuevoEstadoInt);
        }
        else
        {
            return false;
        }

    }
    public void EliminarPedido(int pedidoId)
    {
        Pedido eliminar = this.pedidos.FirstOrDefault(ped => ped.Numero == pedidoId);
        if (eliminar != null)
        {
            this.pedidos.Remove(eliminar);
        }
    }

    public int cantPedidosCadeteEntregados(int id)//Cantidad de pedidos entregados por un cadete (el del id)
    {
        List<Pedido> pedidosCad = this.pedidos.Where(ped => ped.CoincideCadete(id)).ToList();
        return pedidosCad.Count(ped => ped.Estado == EstadoPedido.Entregado);
    }

    private bool EncuentraCadete(Cadete cad) //Defuelfe si un cadete pertenece a la lista de cadetes
    {
        return this.cadetes.FirstOrDefault(cadet => cadet.Id == cad.Id) != null;
    }
    public List<Pedido> PedidosPendientes()
    {
        return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Pendiente).ToList();
    }
    public List<Pedido> PedidosEntregados()
    {
        return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Entregado).ToList();
    }
    public List<Pedido> PedidosRechazados()
    {
        return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Rechazado).ToList();
    }
    public List<Pedido> PedidosSinCadete()
    {
        return this.pedidos.Where(ped => ped.NoTieneCadeteAsignado()).ToList();
    }

    public List<Pedido> GetListaPedidos()
    {
        if (this.pedidos != null)
        {
            return this.pedidos;
        }
        else
        {
            return new List<Pedido>();
        }
    }
    public string MostrarPedidos()
    {
        string pedidosMostrar = "";
        foreach (var ped in this.pedidos)
        {
            pedidosMostrar += "\n - " + ped.MostrarPedido();
        }
        return pedidosMostrar;
    }
    public Pedido GetPedido(int id)
    {
        Pedido? pedido = this.pedidos.FirstOrDefault(ped => ped.Numero == id);
        if (pedido != null)
        {
            return pedido;
        }
        else
        {
            return new Pedido();
        }
    }



    //Cadetes
    public void AgregarCadete(Cadete cadete)
    {
        if (this.cadetes.FirstOrDefault(cad => cad.Id == cadete.Id, null) == null)
        {
            this.cadetes.Add(cadete);
        }
    }
    public Cadete? DevuelveCadete(int numCadete)//Devuelve el cadete que coincide con ID, si es que lo encuentra en la cadeteria, sino devuelve null
    {
        return this.cadetes.FirstOrDefault(cad => cad.Id == numCadete, null);
    }
    public Cadete GetCadete(int id)
    {
        Cadete? cad = this.cadetes.FirstOrDefault(c => c.Id == id, null);
        if (cad == null)
        {
            return new Cadete();
        }
        else
        {
            return cad;
        }
    }
    public List<Cadete> GetListaCadetes()//Devuelve la lista de cadetes
    {
        if (this.cadetes != null)
        {
            return this.cadetes;
        }
        else
        {
            return new List<Cadete>();
        }
    }
    public float JornalACobrar(int id) //Devuelve el monto a cobrar por un cadete (se lo utiliza en el informe)
    {
        float monto = 500 * cantPedidosCadeteEntregados(id);
        return monto;
    }
    public string MuestraCadetes()
    {
        string lista = "";
        foreach (var item in this.cadetes)
        {
            lista += "\t - " + item.Mostrar() + "\n";
        }
        return lista;
    }
    public bool ExisteCadete(Cadete cad)
    {
        Cadete? cadete = this.cadetes.FirstOrDefault(c => c == cad, null);
        return (cadete != null);
    }


    //Informe
    public string Informe()
    {
        List<DatosCadete> datosCadetes = new List<DatosCadete>();
        foreach (var item in this.cadetes)
        {
            DatosCadete datosCad = new DatosCadete(cantPedidosCadeteEntregados(item.Id), JornalACobrar(item.Id), item.Nombre);
            datosCadetes.Add(datosCad);
        }
        var informe = new Informe(datosCadetes);
        return informe.Mostrar();
    }
    public Informe GetInforme()
    {
        List<DatosCadete> datosCadetes = new List<DatosCadete>();
        foreach (var item in this.cadetes)
        {
            DatosCadete datosCad = new DatosCadete(cantPedidosCadeteEntregados(item.Id), JornalACobrar(item.Id), item.Nombre);
            datosCadetes.Add(datosCad);
        }
        var informe = new Informe(datosCadetes);
        return informe;
    }
    public bool EsPedidoPredeterminado(int id)
    {
        Pedido encontrado = this.GetPedido(id);
        if (encontrado.EsPredeterminado())
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    //Guardar pedidos en JSON
    public void GuardarPedidos()
    {
        this.datosPedidos.Guardar(this.pedidos);
    }
    public void GuardarCadetes()
    {
        this.datosCadetes.Guardar(this.cadetes);
    }
}

