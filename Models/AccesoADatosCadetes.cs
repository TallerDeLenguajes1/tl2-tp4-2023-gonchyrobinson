using System.Text.Json;
namespace tl2_tp4_2023_gonchyrobinson;
public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        string rutaDeArchivo= "./CargaArchivos/Cadetes.json";
         List<Cadete> listaCadetes;
        string documento;
        using (var archivoOpen = new FileStream(rutaDeArchivo, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                documento = strReader.ReadToEnd();
                archivoOpen.Close();
            }
            listaCadetes = JsonSerializer.Deserialize<List<Cadete>>(documento);
        }
        return (listaCadetes);
    }
      public void Guardar(List<Cadete> cadetes)
    {
        string datosPedidos ="./CargaArchivos/Cadetes.json";
        string formatoJson = JsonSerializer.Serialize(cadetes);
        File.WriteAllText(datosPedidos, formatoJson);
    }
}