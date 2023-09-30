using System.Text.Json;
namespace tl2_tp4_2023_gonchyrobinson;
public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        string rutaDeArchivo = "./CargaArchivos/Pedidos.json";
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
        string datosPedidos = "./CargaArchivos/Pedidos.json";
        string formatoJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText(datosPedidos, formatoJson);
    }
}