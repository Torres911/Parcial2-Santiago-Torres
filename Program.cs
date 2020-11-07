using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using linq.Torneo;
using Newtonsoft.Json;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Fachada fachada = new Fachada();
            fachada.Menu();
        }
    }
}
