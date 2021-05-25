using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Recursos
{
    public class Singleton
    {
        private static Singleton _instancia;
        public Dictionary<string, object> Recursos { get; set; }

        private Singleton() 
        {
            Recursos = new Dictionary<string, object>();
        }

        public static Singleton ObtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Singleton();
            }

            return _instancia;
        }
    }
}
