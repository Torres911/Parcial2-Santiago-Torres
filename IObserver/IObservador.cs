using System;
using System.Collections.Generic;
using System.Linq;

namespace linq.Torneo
{
    public interface IObservador 
    {
        #region Methods
        public void LlenarEquipos(Partido p);
        #endregion Methods
    }
    
    public interface IListener 
    {
        #region Methods
        public void Subscribe(Seleccion temp); 
        public void Unsubscribe(Seleccion temp); 
        public void Signal(); 
        #endregion Methods
    }
    
}