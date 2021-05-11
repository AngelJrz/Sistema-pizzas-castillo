using AccesoADatos.ControladoresDeDatos;
using AccesoADatos.Excepciones;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class EmpleadoController
    {
        private UsuarioDAO usuarioDAO;

        public EmpleadoController()
        {
            usuarioDAO = new UsuarioDAO();
        }

        public bool RegistrarEmpleado(Empleado empleado)
        {
            empleado.NumeroEmpleado = GenerarNumeroEmpleado(empleado.TipoUsuario);
            empleado.FechaRegistro = DateTime.Now;

            string passwordConHash = AdministradorHash.GenerarHash(empleado.Contrasenia);
            empleado.Contrasenia = passwordConHash;

            AccesoADatos.Persona empleadoARegistrar = Persona.CloneToEntityDB(empleado);
            empleadoARegistrar.Empleado.Add(Empleado.CloneToEntityDB(empleado));

            bool seRegistro;
            try
            {
                seRegistro = usuarioDAO.RegistrarEmpleado(empleadoARegistrar);
            }
            catch (Exception)
            {

                throw;
            }

            return seRegistro;
        }

        private string GenerarNumeroEmpleado(Tipo tipoUsuario)
        {
            string numeroEmpleado = tipoUsuario.ObtenerEtiquetaTipoEmpleado() + "-";
            TipoUsuarioDAO tipoUsuarioDAO = new TipoUsuarioDAO();
            int numeroDeEmpleadosActual = tipoUsuarioDAO.ObtenerNumeroDeTipoUsuario(tipoUsuario.Nombre);
            int nuevoNumeroDeEmpleado = numeroDeEmpleadosActual + 1;

            numeroEmpleado += nuevoNumeroDeEmpleado.ToString("0000");
            return numeroEmpleado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>El empleado que inició sesión o null si no se encontró.</returns>
        public Empleado IniciarSesion(string username, string password)
        {
            Empleado empleado;
            AccesoADatos.Empleado empleadoBd;

            try
            {
                empleadoBd = usuarioDAO.ObtenerEmpleado(username);
            }
            catch (ConexionFallidaException)
            {
                throw;
            } 

            if (empleadoBd == null)
                return null;

            if (!AdministradorHash.CompararHash(password, empleadoBd.Contrasenia))
                return null;

            Persona personaInfo = Persona.Clone(empleadoBd.Persona);
            empleado = Empleado.Clone(empleadoBd);
            empleado.SetInformacionPersonal(personaInfo);

            return empleado;
        }
    }
}
