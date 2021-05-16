using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class ClienteController
    {
        public bool GuardarCliente(Persona persona)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.RegistrarCliente(CloneRegister(persona));
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
            };
        }

<<<<<<< HEAD
        public List<AccesoADatos.Persona> ObtenerClientes() {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.ObtenerClientes();

        }
        public List<AccesoADatos.Persona> ObtenerClientesNombre(string nombre)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.ObtenerClienteNombre(nombre);

        }


=======
        
>>>>>>> ModuloCocina
    }
}
