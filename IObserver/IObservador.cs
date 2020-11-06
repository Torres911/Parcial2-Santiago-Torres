using System;
using System.Collections.Generic;
using System.Linq;

namespace linq.Torneo
{
    public interface IObservador 
    {
        #region Methods
        public void update(Partido p);
        #endregion Methods
    }
    
    public interface IListener 
    {
        #region Methods
        public void registerObserver(Seleccion temp); 
        public void unregisterObserver(Seleccion temp); 
        public void notifyObservers(); 
        #endregion Methods
    }
    
}