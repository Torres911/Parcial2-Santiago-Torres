using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Equipo
    {
        #region Properties  
        public int Goles { get; set; }
        public int Asistencias { get; set; }
        public int Faltas { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
        public Seleccion Seleccion { get; set; }
        public bool EsLocal { get; set; }

        #endregion Properties

        #region Initialize
        public Equipo(Seleccion seleccion)
        {
            this.Seleccion = seleccion;
        }
        #endregion Initialize

        #region Methods
        public void ExpulsarJugador(string name)
        {
            try
            {
                Jugador jugadorExpulsado = Seleccion.Jugadores.First(j => j.Nombre == name);
                TarjetasRojas++;
                if (Seleccion.Jugadores.Count < 8)
                {
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
                Seleccion.Jugadores.Remove(jugadorExpulsado);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador para expulsarlo del equipo " + Seleccion.Nombre);
            }
            
        }

        public void AmonestarJugador(string name){
            try{
                Jugador jugadorAmonestado = Seleccion.Jugadores.First(j => j.Nombre == name);
                TarjetasAmarillas++;
                jugadorAmonestado.Amarillas++;
                if(TarjetasAmarillas == 2){
                    Seleccion.Jugadores.Remove(jugadorAmonestado);
                    TarjetasRojas++;
                    Console.WriteLine("Por acumulacion de amarillas el jugador " + name + " ha sido expulsado");
                }
                if(Seleccion.Jugadores.Count < 8){
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador en el equipo " + Seleccion.Nombre + " para amonestarlo");
            }
        }
        #endregion Methods
    }
}