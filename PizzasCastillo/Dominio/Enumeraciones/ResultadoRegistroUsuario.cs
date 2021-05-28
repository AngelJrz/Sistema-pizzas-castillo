using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enumeraciones
{
    public enum ResultadoRegistroUsuario
    {
        RegistroFallido = 0,
        RegistroExitoso = 1,
        UsuarioYaExiste = 2,
        DireccionNoEspecificada = 3,
        InformacionIncorrecta = 4
    }
}
