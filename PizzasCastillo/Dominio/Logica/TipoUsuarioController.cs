using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using AccesoADatos.ControladoresDeDatos;
using AccesoADatos.Excepciones;

namespace Dominio.Logica
{
    public class TipoUsuarioController
    {
        private TipoUsuarioDAO _tipoUsuarioDAO;
        public TipoUsuarioController()
        {
            _tipoUsuarioDAO = new TipoUsuarioDAO();
        }

        public List<Tipo> ObtenerTiposUsuario()
        {
            
            List<AccesoADatos.TipoUsuario> tiposUsuariosBD = _tipoUsuarioDAO.ObtenerTiposUsuario();

            List<Tipo> tiposUsuario = Tipo.CloneList(tiposUsuariosBD);

            return tiposUsuario;
        }

        public bool RegistrarNuevoTipoUsuario(Tipo nuevoTipoUsuario)
        {
            AccesoADatos.TipoUsuario tipoUsuarioARegistrar = Tipo.CloneToEntityDB(nuevoTipoUsuario);

            bool seRegistro;

            try
            {
                seRegistro = _tipoUsuarioDAO.AgregarNuevoTipoUsuario(tipoUsuarioARegistrar);
            }
            catch (ModificacionDBFallidaException)
            {
                throw;
            }
            catch (ConexionFallidaException)
            {
                throw;
            }

            return seRegistro;
        }

        public bool DarDeBajaTipoUsuario(Tipo tipoUsuario)
        {
            if (tipoUsuario == null)
                return false;

            AccesoADatos.TipoUsuario tipoUsuarioADarDeBaja = Tipo.CloneToEntityDB(tipoUsuario);

            bool seDioDeBaja;

            try
            {
                seDioDeBaja = _tipoUsuarioDAO.DarDeBajaTipoUsuario(tipoUsuarioADarDeBaja);
            }
            catch (ModificacionDBFallidaException)
            {
                throw;
            }
            catch (ConexionFallidaException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return seDioDeBaja;
        }

        public bool EditarTipoUsuario(Tipo tipoUsuario)
        {
            if (tipoUsuario == null)
                return false;

            AccesoADatos.TipoUsuario tipoUsuarioAEditar = Tipo.CloneToEntityDB(tipoUsuario);

            bool seActualizo;

            try
            {
                seActualizo = _tipoUsuarioDAO.EditarTipoUsuario(tipoUsuarioAEditar);
            }
            catch (ModificacionDBFallidaException)
            {
                throw;
            }
            catch (ConexionFallidaException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return seActualizo;
        }
    }
}
