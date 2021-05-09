using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ProveedorDAO
    {
        private PizzasBDEntities _connection;
        private List<Proveedor> _proveedores;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;

        public ProveedorDAO()
        {
            _connection = new PizzasBDEntities();
            _resultado = 0;
        }

        public List<Proveedor> ObtenerProveedoresNombre(Proveedor proveedor)
        {
            _proveedores = _connection.Proveedor
            .Where(provedorio => provedorio.Nombre == proveedor.Nombre)
                .Include("Proveedor")
                .ToList();

            return _proveedores;
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            try
            {
                DireccionDAO direccionDAO = new DireccionDAO();
                List<DireccionProveedor> direcciones = direccionDAO.ObtenerDireccionesProveedor();
                proveedor.DireccionProveedor = direcciones.Last();
                _connection.Entry(proveedor).State = EntityState.Added;
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
