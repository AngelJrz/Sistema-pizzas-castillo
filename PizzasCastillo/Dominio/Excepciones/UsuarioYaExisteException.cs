using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    public class UsuarioYaExisteException : Exception
    {
        public UsuarioYaExisteException(string usuario)
            : base($"El usuario {usuario} ya está relacionado con otro empleado.")
        {

        }
    }
}
