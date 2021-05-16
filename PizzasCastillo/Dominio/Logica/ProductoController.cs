using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using Dominio.Enumeraciones;

namespace Dominio.Logica
{
    public class ProductoController
    {
        private const int DISPONIBLE = 1;
        private ProductoDAO productoDAO;

        public ProductoController()
        {
            productoDAO = new ProductoDAO();
        }

        public ResultadoRegistroProducto GuardarProducto(Producto producto)
        {
            if (productoDAO.ObtenerProducto(producto.CodigoBarra) != null)
            {
                return ResultadoRegistroProducto.CodigoBarraDuplicado;
            }
                
            if(producto.CodigoBarra == null)
            {
                producto.CodigoBarra = AutogenerarCodigoBarra(producto.Nombre, producto.Tipo);
            }
                
            producto.EsPlatillo = false;
            producto.Estatus = DISPONIBLE;

            AccesoADatos.ArticuloVenta productoNuevo = ArticuloVenta.CloneToDBEntity(producto);
            productoNuevo.Producto = Producto.CloneToDBEntity(producto);

            bool seGuardo;

            try
            {
                seGuardo = productoDAO.RegistrarArticulo(productoNuevo);
            }
            catch (Exception)
            {
                throw;
            }

            if (seGuardo == false)
                return ResultadoRegistroProducto.RegistroFallido;

            return ResultadoRegistroProducto.RegistroExitoso;
        }

        //public AccesoADatos.Producto BuscarProductoID(string codigo)
        //{
        //    ProductosDAO productoDAO = new ProductosDAO();

        //     return productoDAO.ObtenerProductosID(codigo);

        //}
        //public List<Producto> ObtenerProductos()
        //{
        //    productoDAO.ObtenerListaProductos();
        //    return productoDAO.ObtenerListaProductos();
        //}

        //public List<AccesoADatos.Producto> BuscarProductosNombre(string nombre)
        //{
        //    ProductosDAO productoDAO = new ProductosDAO();
        //    return productoDAO.ObtenerProductosNombre(nombre);
        //}

        //public List<AccesoADatos.Producto> BuscarProductosCodigo(string codigo)
        //{

        //    return productoDAO.ObtenerProductosCodigo(nombre);

        //}

        private string AutogenerarCodigoBarra(string nombre, Tipo tipo)
        {
            Random azar = new Random();

            string inicio = tipo.ObtenerEtiquetaTipoProducto();
            string codigoGenerado = String.Concat(inicio, nombre.Substring(0, 3).ToUpper());
            codigoGenerado = codigoGenerado + "-" + azar.Next(100,999);
            
            return codigoGenerado;
        }
    }
}
