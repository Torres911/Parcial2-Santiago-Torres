using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using linq.Torneo;
using Newtonsoft.Json;


namespace linq.Torneo{
    
    public class Fachada : IListener{

        public List<Seleccion> selecciones;
        public Partido partido;

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
                Console.WriteLine("El partido quedo: " + local +" "+ partido.Resultado() +" "+ visitante);
                elocal.GolesTotales += partido.GolesLocal();
                elocal.AsistenciasTotales += partido.GolesLocal();
                evisitante.GolesTotales += partido.GolesVisitante();
                evisitante.AsistenciasTotales += partido.GolesVisitante();
                
            }
            catch (InvalidOperationException){
                Console.WriteLine("No existen esos equipos en la lista de selecciones");
            }
        }
        public void Subscribe(Seleccion temp){ 
            selecciones.Add(temp); 
        } 
      
        public void Unsubscribe(Seleccion temp){ 
            selecciones.Remove(temp); 
        }

        public void Signal(){ 
            foreach(Seleccion temp in selecciones){
                temp.update(partido.LastOrDefault() );
            }
        }
    }
}