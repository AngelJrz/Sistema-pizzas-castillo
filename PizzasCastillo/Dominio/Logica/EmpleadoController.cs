using AccesoADatos.ControladoresDeDatos;
using AccesoADatos.Excepciones;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Excepciones;
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
            if (usuarioDAO.ObtenerEmpleado(empleado.Username) != null)
                throw new UsuarioYaExisteException(empleado.Username);

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

        public List<Empleado> ObtenerEmpleadosActivos()
        {
            List<AccesoADatos.Persona> empleadosBd;
            try
            {
                empleadosBd = usuarioDAO.ObtenerEmpleadosActivos();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ConexionFallidaException)
            {
                throw;
            }


            List<Empleado> empleados = Empleado.FullCloneList(empleadosBd);

            return empleados;
        }

        public bool ActualizarEmpleado(Empleado empleadoActualizado, bool seActualizoUsername = false, bool seActualizoPassword = false)
        {
            AccesoADatos.Empleado empleadoBd = usuarioDAO.ObtenerEmpleado(empleadoActualizado.Id);

            if (empleadoBd == null)
                return false;

            if (seActualizoUsername)
            {
                if (usuarioDAO.ObtenerEmpleado(empleadoActualizado.Username) != null)
                    throw new UsuarioYaExisteException(empleadoActualizado.Username);

                empleadoBd.Username = empleadoActualizado.Username;
            }

            if(seActualizoPassword)
            {
                string passwordConHash = AdministradorHash.GenerarHash(empleadoActualizado.Contrasenia);
                empleadoBd.Contrasenia = passwordConHash;
            }

            empleadoBd.Persona.Nombres = empleadoActualizado.Nombres;
            empleadoBd.Persona.Apellidos = empleadoActualizado.Apellidos;
            empleadoBd.Persona.Telefono = empleadoActualizado.Telefono;
            empleadoBd.Persona.Email = empleadoActualizado.Email;
            empleadoBd.Persona.IdTipoUsuario = empleadoActualizado.TipoUsuario.Id;
            empleadoBd.SalarioQuincenal = empleadoActualizado.SalarioQuincenal;
            empleadoBd.Persona.Direccion.ToList()[0].Calle = empleadoActualizado.Direcciones[0].Calle;
            empleadoBd.Persona.Direccion.ToList()[0].Colonia = empleadoActualizado.Direcciones[0].Colonia;
            empleadoBd.Persona.Direccion.ToList()[0].Ciudad = empleadoActualizado.Direcciones[0].Ciudad;
            empleadoBd.Persona.Direccion.ToList()[0].CodigoPostal = empleadoActualizado.Direcciones[0].CodigoPostal;
            empleadoBd.Persona.Direccion.ToList()[0].NumeroInterior = empleadoActualizado.Direcciones[0].NumeroInterior;
            empleadoBd.Persona.Direccion.ToList()[0].Referencias = empleadoActualizado.Direcciones[0].Referencias;
            empleadoBd.Persona.Direccion.ToList()[0].NumeroExterior = empleadoActualizado.Direcciones[0].NumeroExterior;
            empleadoBd.Persona.Direccion.ToList()[0].EntidadFederativa = empleadoActualizado.Direcciones[0].EntidadFederativa;

            bool seActualizo;

            try
            {
                seActualizo = usuarioDAO.ActualizarEmpleado(empleadoBd);
            }
            catch (Exception)
            {
                throw;
            }

            return seActualizo;
        }

        public bool DarDeBajaEmpleado(string numeroEmpleado)
        {
            bool seDioDeBaja;

            try
            {
                seDioDeBaja = usuarioDAO.DarDeBajaEmpleado(numeroEmpleado);
            }
            catch (Exception)
            {
                throw;
            }

            return seDioDeBaja;
        }
    }
}
