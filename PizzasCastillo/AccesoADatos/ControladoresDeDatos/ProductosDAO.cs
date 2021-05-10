using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ProductosDAO
    {
        private PizzasBDEntities _connection;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;




        public bool RegistrarProducto(Producto productoAregistrar)
        {
            try
            {

                _connection.Entry(productoAregistrar).State= EntityState.Added;
                _connection.SaveChangesAsync();
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
