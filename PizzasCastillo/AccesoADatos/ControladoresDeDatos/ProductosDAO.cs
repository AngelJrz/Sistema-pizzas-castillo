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

        private Producto productoEncontrado;
        private List<Producto> _productos;

        public ProductosDAO()
        {
            _connection = new PizzasBDEntities();
        }


        public List<Producto> ObtenerListaProductos()
        {
            _productos = _connection.Producto.ToList();

            return _productos;
        }



        public Producto ObtenerProductosID(string codigoBarras)
        {
            try
            {
                productoEncontrado = (Producto)_connection.Producto
                .Where(p => p.CodigoBarra == codigoBarras);

            }
            catch (Exception){

                throw;
            }
            return productoEncontrado;

        }

        public List<Producto> ObtenerProductosNombre(string nombre) {

             _productos = _connection.Producto
                    .Where(lista => lista.ArticuloVenta.Nombre.Contains(nombre)).ToList();

                    return _productos;
        }

        public bool ActualizarInventario(List<Producto> listaDeProductos)
        {
            bool registrado = false;
            try
            {
                foreach (Producto productoActualizado in listaDeProductos)
                {
                    Producto productoDB  = _connection.Producto.Where(producto => producto.CodigoBarra.Equals(productoActualizado.CodigoBarra)).SingleOrDefault();
                    productoDB.Cantidad = productoActualizado.Cantidad;
                    _connection.Entry(productoDB).State = EntityState.Modified;
                }

                _connection.SaveChanges();
                registrado = true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return registrado;

        }

    }




}
