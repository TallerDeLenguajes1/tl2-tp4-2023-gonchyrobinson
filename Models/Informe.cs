namespace tl2_tp4_2023_gonchyrobinson;

    public class Informe{
        private List<DatosCadete> datosCadetes;
        private float enviosPromedioPorCadete;
        private int totalEnvios;
        private float totalMontoGanado;

    public List<DatosCadete> DatosCadetes { get => datosCadetes; set => datosCadetes = value; }
    public float EnviosPromedioPorCadete { get => enviosPromedioPorCadete; set => enviosPromedioPorCadete = value; }
    public int TotalEnvios { get => totalEnvios; set => totalEnvios = value; }
    public float TotalMontoGanado { get => totalMontoGanado; set => totalMontoGanado = value; }

    public Informe(List<DatosCadete> datosCadetes){
            this.DatosCadetes=datosCadetes;
            this.TotalEnvios=datosCadetes.Sum(cliente => cliente.CantEnvios);
            this.TotalMontoGanado=datosCadetes.Sum(cliente => cliente.Monto);
            this.EnviosPromedioPorCadete=(float)this.TotalEnvios/(float)datosCadetes.Count();
        }
        public Informe(){
            this.DatosCadetes=new List<DatosCadete>();
            this.TotalEnvios=0;
            this.TotalMontoGanado=0;
            this.EnviosPromedioPorCadete=0;
        }
        public void AgregarCadete(DatosCadete datosCadete){
            this.DatosCadetes.Add(datosCadete);
            this.TotalEnvios=this.DatosCadetes.Sum(cliente => cliente.CantEnvios);
            this.TotalMontoGanado=this.DatosCadetes.Sum(cliente => cliente.Monto);
            this.EnviosPromedioPorCadete=(float)this.TotalEnvios/(float)this.DatosCadetes.Count();
        }
        public string Mostrar(){
            string datos ="-------------------DATOS DE CADA CADETE-----------------------\n";
            foreach (var item in this.DatosCadetes)
            {
                datos+="\t"+item.Mostrar()+"\n";
            }
            datos+="------------------------DATOS DE LA CADETERIA-------------------------\n\tTotal de envios: "+this.TotalEnvios+"\n\tTotal a ganado: "+this.TotalMontoGanado+"\n\tEnvios Promedio por cadete: "+this.EnviosPromedioPorCadete+"\n\n";
            return datos;
        }
    }


    