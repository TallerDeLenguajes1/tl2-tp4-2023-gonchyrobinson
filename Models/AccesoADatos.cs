using System.IO;
using System.Text;
namespace tl2_tp4_2023_gonchyrobinson;
    public abstract class AccesoADatos
    {
        public virtual List<Cadeteria> AccesoCadeterias(string rutaDeArchivo){
            return new List<Cadeteria>();
        }
        public virtual List<Cadete> AccesoCadetes(string rutaDeArchivo){
            return new List<Cadete>();
        }
        public virtual void EscribirJSONCadeteria(string rutaDeArchivo, List<Cadeteria> cadeteria)
        {
        }
        public virtual void EscribirJSONCadetes(string rutaDeArchivo, List<Cadete> cadetes)
        {
        }
        
    }