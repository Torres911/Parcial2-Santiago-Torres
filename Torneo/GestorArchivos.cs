using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using linq.Torneo;
using Newtonsoft.Json;


namespace linq.Torneo{
    
    public class GestorArchivos{

        public List<Seleccion> CargarDeFREDDY(){
            try{
                List<Seleccion> seleccionesDeserializadas = JsonConvert.DeserializeObject<List<Seleccion>>(File.ReadAllText("./selecciones.json"));
                return seleccionesDeserializadas;
            }
            catch (FileNotFoundException fnf){
                Console.WriteLine("El archivo (selecciones.json) no existe dentro de la memoria");
            }
            
        }

        public void SubirAlFREDDY(List<Seleccion> sel){
            try{
                File.WriteAllText("./selecciones.json", JsonConvert.SerializeObject(sel));
            }
            catch (NullReferenceException e){
                Console.WriteLine("La lista de selecciones se encuentra vacia");
            }
            
        }
    }
}