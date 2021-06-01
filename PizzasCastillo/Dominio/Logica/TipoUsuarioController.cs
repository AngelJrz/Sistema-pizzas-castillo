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

        public ResultadoRegistro RegistrarNuevoTipoUsuario(Tipo nuevoTipoUsuario)
        {
            if (nuevoTipoUsuario == null || String.IsNullOrWhiteSpace(nuevoTipoUsuario.Nombre))
                return ResultadoRegistro.InformacionIncorrecta;

            if (_tipoUsuarioDAO.ObtenerTipoUsuario(nuevoTipoUsuario.Nombre) != null)
                return ResultadoRegistro.TipoUsuarioYaExiste;

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

            if (!seRegistro)
                return ResultadoRegistro.RegistroFallido;

            return ResultadoRegistro.RegistroExitoso;
        }

        public ResultadoRegistro DarDeBajaTipoUsuario(Tipo tipoUsuario)
        {
            int VALOR_MINIMO_ID = 1;
            if (tipoUsuario == null || tipoUsuario.Id < VALOR_MINIMO_ID)
                return ResultadoRegistro.InformacionIncorrecta;

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

            if (!seDioDeBaja)
                return ResultadoRegistro.RegistroFallido;


            return ResultadoRegistro.RegistroExitoso;
        }

        public ResultadoRegistro EditarTipoUsuario(Tipo tipoUsuario, bool seActualizoNombre = false)
        {
            if (tipoUsuario == null
                || String.IsNullOrWhiteSpace(tipoUsuario.Nombre)
                || tipoUsuario.Estatus < 0
                || tipoUsuario.Estatus > 1)
                return ResultadoRegistro.InformacionIncorrecta;

            if (seActualizoNombre)
            {
                if (_tipoUsuarioDAO.ObtenerTipoUsuario(tipoUsuario.Nombre) != null)
                    return ResultadoRegistro.TipoUsuarioYaExiste;
            }

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

            if (!seActualizo)
                return ResultadoRegistro.RegistroFallido;

            return ResultadoRegistro.RegistroExitoso;
        }
    }
}
