namespace tl2_tp4_2023_gonchyrobinson;
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private string telefono;
        private string datosReferenciaDireccion;

        public Cliente(string? nombre, string? direccion, string? telefono, string? datosReferenciaDireccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }
        public Cliente(){
            
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
        public string VerDatos(){
            return("DATOS: \n\tNombre: "+this.nombre+"\n\tDireccion: "+this.direccion+"\n\tTelefono: "+this.telefono+"\n\t\tDatos de referencia direccion: "+this.datosReferenciaDireccion);
        }
        public string VerDireccion(){
            return this.direccion;
        }
    }
