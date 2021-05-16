using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;

namespace Dominio.Logica
{
    class ProductoController
    {
        private const int DISPONIBLE = 1;

        public bool GuardarProducto(Producto producto)
        {
            ProductoDAO productoDAO = new ProductoDAO();
            bool seGuardoArticulo = productoDAO.RegistrarArticulo(CloneRegisterArticulo(producto));
            bool seGuardoProducto = productoDAO.RegistrarProducto(CloneRegisterProducto(producto));
            if(seGuardoArticulo && seGuardoProducto)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private AccesoADatos.Producto CloneRegisterProducto(Producto producto)
        {
            return new AccesoADatos.Producto
            {
                CodigoBarra = producto.CodigoBarra,
                Cantidad = producto.Cantidad,
                Descripcion = producto.Descripcion,
                PrecioCompra = producto.PrecioCompra,
                Restricciones = producto.Restricciones,
                IdTipoProducto = producto.Tipo.Id,
                UnidadDeMedida = producto.UnidadDeMedida
            };
        }

        private AccesoADatos.ArticuloVenta CloneRegisterArticulo(Producto producto)
        {
            return new AccesoADatos.ArticuloVenta
            {
                CodigoBarra = producto.CodigoBarra,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Foto = producto.Foto,
                Estatus = DISPONIBLE,
                EsPlatillo = false,
            };
        }
       
        public AccesoADatos.Producto BuscarProductoID(string codigo)
        {
            ProductosDAO productoDAO = new ProductosDAO();

             return productoDAO.ObtenerProductosID(codigo);
            
        }
        public List<AccesoADatos.Producto> ObtenerProductos()
        {
            ProductosDAO productoDAO = new ProductosDAO();

            return productoDAO.ObtenerListaProductos();

        }

        public List<AccesoADatos.Producto> BuscarProductosNombre(string nombre)
        {
            ProductosDAO productoDAO = new ProductosDAO();

            return productoDAO.ObtenerProductosNombre(nombre);

        }
    }
}
