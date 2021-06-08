using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Entidades.Persona;

namespace Dominio.Logica
{
    public class ClienteController
    {
     
        private List<AccesoADatos.Persona> clientesSinclone;

        public ResultadoRegistro RegistrarCliente(Persona persona)
        {
            if (persona == null)
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            if (persona.Direcciones == null || persona.Direcciones.Count == 0)
            {
                return ResultadoRegistro.DireccionNoEspecificada;
            }

            if (!EstaInformacionCorrecta(persona))
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            ClienteDAO clienteDAO = new ClienteDAO();

            if (clienteDAO.ObtenerClienteTelefono(persona.Telefono) != null)
            {
                return ResultadoRegistro.UsuarioYaExiste;
            }

            bool seRegistro;
            try
            {
                seRegistro = clienteDAO.RegistrarCliente(CloneRegister(persona));
            }
            catch (Exception)
            {
                throw;
            }

            if (!seRegistro)
            {
                return ResultadoRegistro.RegistroFallido;

            }
            else
            {
                return ResultadoRegistro.RegistroExitoso;
            }
        }

        private bool EstaInformacionCorrecta(Persona persona)
        {
            ValidadorPersonas validadorPersona = new ValidadorPersonas();
            ValidadorDireccion validadorDireccion = new ValidadorDireccion();

            return validadorPersona.Validar(persona) &&
                validadorDireccion.Validar(persona.Direcciones[0]);
        }

        private AccesoADatos.Persona CloneRegister(Persona persona)
        {
            return new AccesoADatos.Persona
            {
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Estatus = 1,
                Direccion = Direccion.CloneToEntityDBList(persona.Direcciones),
            };

        }

        public List<Persona> ObtenerPersonas()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            List<AccesoADatos.Persona> clientesBD = clienteDAO.ObtenerClientes();

            List<Persona> clientes = Persona.CloneListPersona(clientesBD);

            return clientes;
        }
        public List<AccesoADatos.Persona> ObtenerClientesNombre(string nombre)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.ObtenerClienteNombre(nombre);

        }
    }
}
