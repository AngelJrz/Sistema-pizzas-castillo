using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.Excepciones
{
    public class ValidacionEntityException : Exception
    {
        public ValidacionEntityException(string message)
            : base(message)
        {

        }
    }
}
