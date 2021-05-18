using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
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
