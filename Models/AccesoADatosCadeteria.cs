using tl2_tp4_2023_gonchyrobinson.Controllers;
using System.Text.Json;
using System.IO;
namespace tl2_tp4_2023_gonchyrobinson;
public class AccesoADatosCadeteria{
    public Cadeteria Obtener(){
        List<Cadeteria> listaProductos;
        string rutaDeArchivo = "./CargaArchivos/Cadeterias.json";
        string documento;
            using (var archivoOpen = new FileStream(rutaDeArchivo, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    documento = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
                listaProductos = JsonSerializer.Deserialize<List<Cadeteria>>(documento);
            }
        Cadeteria elegida;
        Random rand=new Random();
        elegida = listaProductos[rand.Next(0,listaProductos.Count())];
        return elegida;
    } 
}
