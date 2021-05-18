using AccesoADatos.Excepciones;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        private const int NO_ACTIVO = 0;
        private int _resultado;

        public UsuarioDAO()
        {
            _connection = new PizzasBDEntities();
            _resultado = 0;
        }

        public List<Persona> ObtenerEmpleadosActivos()
        {
            try
            {
                _empleados = _connection.Persona
                .Where(persona => persona.Estatus == ACTIVO)
                .Include("Empleado")
                .ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }
            

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
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
            {
                return false;
            }

            return true;
        }

        public Empleado ObtenerEmpleado(string username)
        {
            Empleado empleadoBuscado;

            try
            {
                empleadoBuscado = _connection.Empleado
                    .Where(empleado => empleado.Username.Equals(username))
                    .Where(empleado => empleado.Persona.Estatus == 1)
                    .Include(empleado => empleado.Persona)
                    .FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch(EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }
            

            return empleadoBuscado;
        }

        public Empleado ObtenerEmpleado(int id)
        {
            Empleado empleadoBuscado;

            try
            {
                empleadoBuscado = _connection.Empleado
                    .Where(empleado => empleado.Persona.Id == id)
                    .Include(empleado => empleado.Persona)
                    .FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }


            return empleadoBuscado;
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            if (empleado == null)
                return false;

            _connection.Entry(empleado).State = EntityState.Modified;

            try
            {
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
                return false;

            return true;
        }

        public bool DarDeBajaEmpleado(string numeroEmpleado)
        {
            if (String.IsNullOrEmpty(numeroEmpleado))
                return false;

            Empleado empleadoBuscado;

            try
            {
                empleadoBuscado = _connection.Empleado
                    .Where(empleado => empleado.NumeroEmpleado.Equals(numeroEmpleado))
                    .Include("Persona")
                    .FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (empleadoBuscado == null)
                return false;

            empleadoBuscado.Persona.Estatus = NO_ACTIVO;

            _connection.Entry(empleadoBuscado).State = EntityState.Modified;

            try
            {
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
                return false;

            return true;
        }
    }
}
