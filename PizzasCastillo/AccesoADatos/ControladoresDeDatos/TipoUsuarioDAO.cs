using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class TipoUsuarioDAO
    {
        private PizzasBDEntities conexion;
        private List<TipoUsuario> tiposUsuario;

        public TipoUsuarioDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public List<TipoUsuario> ObtenerTiposUsuario()
        {
            try
            {
                tiposUsuario = conexion.TipoUsuario.ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            

            return tiposUsuario;
        }

        public int ObtenerNumeroDeTipoUsuario(string nombreTipoUsuario)
        {
            return conexion.Persona.Where(empleado => empleado.TipoUsuario.Nombre.Equals(nombreTipoUsuario))
                .ToList().Count;
        }
    }
}
