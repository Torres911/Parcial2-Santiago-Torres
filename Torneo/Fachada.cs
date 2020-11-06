using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using linq.Torneo;
using Newtonsoft.Json;


namespace linq.Torneo{
    
    public class Fachada{

        private List<Seleccion> selecciones;
        private Partido partido;

        public void CargarDeFREDDY(){
            try{
                selecciones = JsonConvert.DeserializeObject<List<Seleccion>>(File.ReadAllText("./selecciones.json"));
            }
            catch (FileNotFoundException fnf){
                Console.WriteLine("El archivo (selecciones.json) no existe dentro de la memoria");
            }
        }

        public void SubirAlFREDDY(){
            File.WriteAllText("./selecciones.json", JsonConvert.SerializeObject(selecciones));
        }

        public void crearPartido(string local, string visitante){
            try{
                Seleccion elocal = selecciones.First(s => s.Nombre == local) as Seleccion;
                Seleccion evisitante = selecciones.First(s => s.Nombre == visitante) as Seleccion;
                partido = new Partido(elocal, evisitante);
                partido.EquipoLocal.Goles;
                Console.WriteLine("El partido quedo: " + local +" "+ partido.Resultado() +" "+ visitante);
            }
            catch (InvalidOperationException){
                Console.WriteLine("No existen esos equipos en la lista de selecciones");
            }
        }
    }
}