namespace tl2_tp4_2023_gonchyrobinson;
public enum EstadoPedido
{
    Pendiente = 0,
    Entregado = 1,
    Rechazado = 2
}
public class Pedido
{
    //Variables privadas
    private int numero;
    private string obs;
    private EstadoPedido estado;
    private Cliente cliente;
    private Cadete cadete;
    //Variables públicas
    public int Numero { get => numero; set => numero = value; }
    public string Obs { get => obs; set => obs = value; }
    public EstadoPedido Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }



    //Constructores
    public Pedido()
    {
        this.numero = 0;
        this.obs = "No existe";
        this.estado = EstadoPedido.Rechazado;
        this.cliente = new Cliente();
        this.cadete = new Cadete();
    }
    public Pedido(int numero, string obs, Cliente cliente, EstadoPedido estado, Cadete cadete)
    {
        this.numero = numero;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = estado;
        this.cadete = cadete;
    }
    public Pedido(int numero, string obs, Cliente cliente, int estadoInt, Cadete cadete)
    {
        EstadoPedido estado = Pedido.ConvierteIntAEnum(estadoInt);
        this.estado = estado;
        this.numero = numero;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = estado;
        this.cadete = cadete;
    }
    public Pedido(int numero, string obs, Cliente cliente, Cadete cadete)
    {
        this.numero = numero;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = EstadoPedido.Pendiente;
        this.cadete = cadete;

    }
    public Pedido(int numero, string obs, Cliente cliente)
    {
        this.numero = numero;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = EstadoPedido.Pendiente;
        this.cadete = null;

    }



    //Funciones estáticas
    public static EstadoPedido ConvierteIntAEnum(int estadoInt)
    {
        EstadoPedido estadoEnum;
        if (estadoInt == 0 || estadoInt == 1 || estadoInt == 2)
        {
            switch (estadoInt)
            {
                case 0:
                    estadoEnum = EstadoPedido.Pendiente;
                    break;
                case 1:
                    estadoEnum = EstadoPedido.Entregado;
                    break;
                case 2:
                    estadoEnum = EstadoPedido.Rechazado;
                    break;
                default:
                    estadoEnum = EstadoPedido.Pendiente;
                    break;
            }
            return estadoEnum;
        }
        return EstadoPedido.Pendiente;
    }




    //Pedidos
    public void ActualizarPedido(EstadoPedido estado)
    {
        this.estado = estado;
    }
    public void ActualizarPedido(int estadoInt)
    {
        EstadoPedido estadoEnum = ConvierteIntAEnum(estadoInt);
        this.estado = estadoEnum;

    }
    public string MostrarPedido()
    {
        string devuelve = "Numero: " + this.numero + " - Estado: " + this.estado + " - Observaciones: " + this.obs + " - // Cadete: ";
        if (this.cadete != null)
        {
            devuelve += this.cadete.Mostrar();
        }
        else
        {
            devuelve += "No asignado";
        }
        return devuelve;
    }
    public int GetNumero()
    {
        return this.numero;
    }    
    
    
    //Relacionan pedido y cadete
    public void AsignarCadeteAPedido(Cadete cadete)
    {
        this.cadete = cadete;
    }
    public bool CoincideCadete(int idCadete)
    {
        if (this.cadete != null)
        {
            return this.cadete.Id == idCadete;
        }
        else
        {
            return false;
        }
    }

    public bool NoTieneCadeteAsignado()
    {
        return this.cadete == null;
    }
    public bool CoincideCadete(Cadete cadete)
    {
        return this.cadete == cadete;
    }


    //Clientes
    public string VerDireccionCliente()
    {
        return this.cliente.VerDireccion();
    }
    public string VerDatosCliente()
    {
        return this.cliente.VerDatos();
    }
    public bool EsPredeterminado(){
        return(this.numero==0 && this.obs =="No existe" && this.estado==EstadoPedido.Rechazado);
    }


}