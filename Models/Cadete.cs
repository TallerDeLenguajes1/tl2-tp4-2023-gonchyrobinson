namespace tl2_tp4_2023_gonchyrobinson;
public partial class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

//Constructores
    public Cadete(int id, string nombre, string direccion, string telefono, List<Pedido> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }
    public Cadete()
    {

    }
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

//Funciones para mostrar
    public string Mostrar()
    {
        return "ID: " + this.id + " - Nombre: " + this.nombre + " - Telefono: " + this.telefono + " - Direccion: " + this.direccion;
    }
}
