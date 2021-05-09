using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class DireccionDAO
    {

        private PizzasBDEntities _connection;
        private List<Direccion> _direcciones;
        private List<DireccionProveedor> _direccionesProveedor;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;

        public DireccionDAO()
        {
            _connection = new PizzasBDEntities();
            _resultado = 0;
        }

        public List<Direccion> ObtenerDireccionesCliente(Persona persona)
        {
                _direcciones = _connection.Direccion
                .Where(direccion => direccion.IdPersona == persona.Id)
                .ToList();

            return _direcciones;
        }

        public bool RegistrarDireccionCliente(Direccion direccion, string telefono)
        {
            try
            {
                Persona persona = (Persona)_connection.Persona.Where(clientelo => clientelo.Telefono == telefono);
                direccion.IdPersona = persona.Id;
                _connection.Entry(direccion).State = EntityState.Added;
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }

        public List<DireccionProveedor> ObtenerDireccionesProveedor()
        {
            _direccionesProveedor = _connection.DireccionProveedor
            .ToList();

            return _direccionesProveedor;
        }

        public bool RegistrarDireccionProveedor(DireccionProveedor direccion)
        {
            try
            {
                _connection.Entry(direccion).State = EntityState.Added;
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }

    }
}
