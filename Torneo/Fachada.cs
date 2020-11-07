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
                Signal();
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
                temp.LlenarEquipos(partido);
            }
        }
        public void Menu(){
            int opc;
            do{
                Console.WriteLine("***************************************************************************");
                Console.WriteLine("                Bienvenido al Simulador de Torneo de Football");
                Console.WriteLine("***************************************************************************");
                Console.WriteLine("Opciones");
                Console.WriteLine("1. Cargar al sistema las selecciones.");
                Console.WriteLine("2. Cargar al archivo las selecciones.");
                Console.WriteLine("3. Generar Partido.");
                Console.WriteLine("4. Salir.");
                Console.WriteLine("***************************************************************************");
                Console.WriteLine("Digite la opcion que desea realizar dentro del simulador:");
                opc = int.Parse(Console.ReadLine());
                Console.WriteLine("***************************************************************************");

                switch (opc){
                    case 1:
                        Console.WriteLine("***************************************************************************");
                        CargarDeFREDDY();
                        Console.WriteLine("***************************************************************************");
                        break;
                    case 2:
                        Console.WriteLine("***************************************************************************");
                        SubirAlFREDDY();
                        Console.WriteLine("***************************************************************************");
                        break;
                    case 3:
                        Console.WriteLine("***************************************************************************");
                        String e1, e2;
                        Console.WriteLine("Digite el nombre de la seleccion que desea poner de local");
                        e1 = Console.ReadLine();
                        Console.WriteLine("Digite el nombre de la seleccion que desea poner de visitante");
                        e2 = Console.ReadLine();
                        crearPartido(e1,e2);
                        Console.WriteLine("***************************************************************************");
                        break;
                    case 4:
                        Console.WriteLine("***************************************************************************");
                        Console.WriteLine("           ¡Gracias por usar el Simulador de Torneo de Football!");
                        Console.WriteLine("***************************************************************************");
                        break;
                    default:
                        Console.WriteLine("***************************************************************************");
                        Console.WriteLine("                ¡Digite una opcion apropiada del menu!");
                        Console.WriteLine("***************************************************************************");
                        break;
                }
            } while (opc != 4);
        }
    }
}