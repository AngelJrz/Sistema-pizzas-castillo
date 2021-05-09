using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class TipoUsuarioController
    {
        public TipoUsuarioController()
        {

        }

        public List<Tipo> ObtenerTiposUsuario()
        {
            TipoUsuarioDAO tipoUsuarioDAO = new TipoUsuarioDAO();
            List<AccesoADatos.TipoUsuario> tiposUsuariosBD = tipoUsuarioDAO.ObtenerTiposUsuario();

            List<Tipo> tiposUsuario = Tipo.CloneList(tiposUsuariosBD);

            return tiposUsuario;
        }
    }
}
