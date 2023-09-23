namespace tl2_tp4_2023_gonchyrobinson;
    public class DatosCadete
    {
//Variables privadas
        private float monto;
        private int cantEnvios;
        private string nombre;
//Variables públicas
        public float Monto { get => monto; set => monto = value; }
        public int CantEnvios { get => cantEnvios; set => cantEnvios = value; }
        public string Nombre { get => nombre; set => nombre = value; }


//Constructor
    public DatosCadete(int cantEnvios, float monto, string nombre)
        {
            this.cantEnvios = cantEnvios;
            this.monto = monto;
            this.nombre = nombre;
        }

//Mostrar datos
        public string Mostrar()
        {
            return "Nombre: " + this.nombre + "\n\tTotal de envios realizados: " + this.cantEnvios + "\t-\tMonto a cobrar: " + this.monto;
        }
    }
