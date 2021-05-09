using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class UsuarioDAO
    {
        private PizzasBDEntities _connection;
        private List<Persona> _empleados;
        private const int ACTIVO = 1;
        private const int NINGUN_CAMBIO_REALIZADO = 0;
        private int _resultado;

        public UsuarioDAO()
        {
            _connection = new PizzasBDEntities();
            _resultado = 0;
        }

        public List<Persona> ObtenerEmpleadosActivos()
        {
            _empleados = _connection.Persona
                .Where(persona => persona.Estatus == ACTIVO)
                .Include("Empleado")
                .ToList();

            return _empleados;
        }

        public bool RegistrarEmpleado(Persona empleado)
        {
            _connection.Entry(empleado).State = EntityState.Added;

            try
            {
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
            {
                return false;
            }

            return true;
        }
    }
}
