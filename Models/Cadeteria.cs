using System;
using System.Collections;
namespace tl2_tp4_2023_gonchyrobinson;
public class Cadeteria
{
    private static Cadeteria cadeteriaSingleton;
    private string nombre;
    private string direccion;
    private List<Cadete> cadetes;

    private List<Pedido> pedidos;
    public static Cadeteria GetCadeteriaSingleton(string rutaCadetes, string rutaCadeterias)
    {
        if (cadeteriaSingleton == null)
        {
            AccesoADatos helper = new AccesoJson();
            Random rand = new Random();
            List<Cadeteria> cadeterias = helper.AccesoCadeterias(rutaCadeterias);
            cadeteriaSingleton = cadeterias[rand.Next(0, cadeterias.Count())];
            cadeteriaSingleton.AgregaCadetesJson(rutaCadetes);
        }
        return cadeteriaSingleton;
    }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

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
    public void EliminarPedido(int pedidoId)
    {
        Pedido eliminar = this.pedidos.FirstOrDefault(ped => ped.Numero == pedidoId);
        if (eliminar != null)
        {
            this.pedidos.Remove(eliminar);
        }
    }
    private void AgregarCadete(Cadete cadete)
    {
        this.cadetes.Add(cadete);
    }
    public Cadete? DevuelveCadete(int numCadete)
    {
        return this.cadetes.FirstOrDefault(cad => cad.Id == numCadete, null);
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
    public bool ActualizarPedido(int idElegido, int nuevoEstadoInt)
    {
        Pedido actualizar = this.pedidos.FirstOrDefault(ped => ped.Numero == idElegido,null);
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
    public float JornalACobrar(int id)
    {
        float monto = 500 * cantPedidosCadeteEntregados(id);
        return monto;
    }
    public int cantPedidosCadeteEntregados(int id)
    {
        List<Pedido> pedidosCad = this.pedidos.Where(ped => ped.CoincideCadete(id)).ToList();
        return pedidosCad.Count(ped => ped.Estado == EstadoPedido.Entregado);
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

    private bool EncuentraCadete(Cadete cad)
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
    public void AgregaCadetes(int tipoArchivo)
    {
        AccesoADatos helper;
        List<Cadete> cadetes;
        string rutaCadeterias;
        string rutaCadetes;
        if (tipoArchivo == 1)
        {
            rutaCadeterias = "Cadeterias.csv";
            rutaCadetes = "Nombres.csv";
            helper = new AccesoCSV();
        }
        else
        {
            rutaCadeterias = "Cadeterias.json";
            rutaCadetes = "Cadetes.json";
            helper = new AccesoJson();
        }
        cadetes = helper.AccesoCadetes(rutaCadetes);
        Random rand = new Random();
        int cant = rand.Next() % cadetes.Count();
        List<Cadete> cadetesAgregar = new List<Cadete>();
        for (int i = 0; i < cant; i++)
        {
            Cadete cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            while (this.EncuentraCadete(cadAgregar))
            {
                cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            }
            this.AgregarCadete(cadAgregar);
        }
    }
    public void AgregaCadetesJson(string ruta)
    {
        AccesoADatos helper = new AccesoJson();
        List<Cadete> cadetes = helper.AccesoCadetes(ruta);
        Random rand = new Random();
        int cant = rand.Next() % cadetes.Count();
        List<Cadete> cadetesAgregar = new List<Cadete>();
        for (int i = 0; i < cant; i++)
        {
            Cadete cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            while (this.EncuentraCadete(cadAgregar))
            {
                cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            }
            this.AgregarCadete(cadAgregar);
        }
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
    public List<Cadete> GetListaCadetes()
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
}

