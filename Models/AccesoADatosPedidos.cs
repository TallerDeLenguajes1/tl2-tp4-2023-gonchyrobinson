using System.Text.Json;
namespace tl2_tp4_2023_gonchyrobinson;
public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        string rutaDeArchivo = "./CargaArchivos/Cadetes.json";
        List<Pedido> listaPedidos;
        string documento;
        using (var archivoOpen = new FileStream(rutaDeArchivo, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                documento = strReader.ReadToEnd();
                archivoOpen.Close();
            }
            listaPedidos = JsonSerializer.Deserialize<List<Pedido>>(documento);
        }
        return (listaPedidos);
    }
    public void Guardar(List<Pedido> pedidos)
    {
        string nombreArchivo = "./CargaArchivos/Pedidos.json";
        string datos = JsonSerializer.Serialize(pedidos);
        using (var archivo = new FileStream(nombreArchivo, FileMode.OpenOrCreate))
        {
            using (var strWriter = new StreamWriter(archivo))
            {
                strWriter.WriteLine("{0}", datos);
                strWriter.Close();
            }
        }

    }
}