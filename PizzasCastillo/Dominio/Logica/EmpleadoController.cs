using AccesoADatos.ControladoresDeDatos;
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
        public bool RegistrarEmpleado(Empleado empleado)
        {
            empleado.NumeroEmpleado = GenerarNumeroEmpleado(empleado.TipoUsuario);
            empleado.FechaRegistro = DateTime.Now;

            string passwordConHash = AdministradorHash.GenerarHash(empleado.Contrasenia);
            empleado.Contrasenia = passwordConHash;

            AccesoADatos.Persona empleadoARegistrar = Persona.CloneToEntityDB(empleado);
            empleadoARegistrar.Empleado.Add(Empleado.CloneToEntityDB(empleado));

            UsuarioDAO usuarioDAO = new UsuarioDAO();
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
    }
}
