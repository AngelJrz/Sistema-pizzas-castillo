using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.Excepciones
{
    public class ConexionFallidaException : Exception
    {
        public ConexionFallidaException(Exception ex)
            : base(ex.Message)
        {

        }
    }
}