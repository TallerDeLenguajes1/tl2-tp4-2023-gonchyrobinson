namespace tl2_tp4_2023_gonchyrobinson;
    public class DatosCadete
    {
        private float monto;
        private int cantEnvios;
        private string nombre;

        public float Monto { get => monto; set => monto = value; }
        public int CantEnvios { get => cantEnvios; set => cantEnvios = value; }

        public DatosCadete(int cantEnvios, float monto, string nombre)
        {
            this.cantEnvios = cantEnvios;
            this.monto = monto;
            this.nombre = nombre;
        }
        public string Mostrar()
        {
            return "Nombre: " + this.nombre + "\n\tTotal de envios realizados: " + this.cantEnvios + "\t-\tMonto a cobrar: " + this.monto;
        }
    }
