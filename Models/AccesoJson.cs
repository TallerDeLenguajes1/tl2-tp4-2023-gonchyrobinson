
using System.Text.Json;
namespace tl2_tp4_2023_gonchyrobinson;
    public class AccesoJson: AccesoADatos{
        public override List<Cadeteria> AccesoCadeterias(string rutaDeArchivo){
            List<Cadeteria> listaProductos;
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
            return (listaProductos);
        }
        public override List<Cadete> AccesoCadetes(string rutaDeArchivo){
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
        public override void EscribirJSONCadeteria(string rutaDeArchivo, List<Cadeteria> cadeteria)
        {
            string datos = JsonSerializer.Serialize(cadeteria);
            using (var archivo = new FileStream(rutaDeArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("{0}", datos);
                    strWriter.Close();
                }
            }
        }
        public override void EscribirJSONCadetes(string rutaDeArchivo, List<Cadete> cadetes)
        {
            string datos = JsonSerializer.Serialize(cadetes);
            using (var archivo = new FileStream(rutaDeArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("{0}", datos);
                    strWriter.Close();
                }
            }
        }
    }
