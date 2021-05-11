using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;

namespace Dominio.Logica
{
    public class ProductoController
    {

       
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
