using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using AccesoADatos.Excepciones;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ProductoDAO
    {
        private PizzasBDEntities conexion;
        private List<ArticuloVenta> listaProductos;
        private const int CREADO = 1;
        private const int SIN_CAMBIOS = 0;
        private const int ACTIVO = 1;
        private int resultado;

        public ProductoDAO()
        {
            conexion = new PizzasBDEntities();
            resultado = 0;
        }

        public List<ArticuloVenta> ObtenerListaProductos()
        {
            listaProductos = conexion.ArticuloVenta
                .Where(producto => producto.EsPlatillo == false)
                .Include(producto => producto.Producto)
                .ToList();

            return listaProductos;
        }

        public List<ArticuloVenta> ObtenerListaProductosActivos()
        {
            listaProductos = conexion.ArticuloVenta
                .Where(producto => producto.EsPlatillo == false)
                .Where(producto => producto.Estatus == ACTIVO)
                .Include(producto => producto.Producto)
                .ToList();

            return listaProductos;
        }

        public List<ArticuloVenta> ObtenerProductosPorNombre(string nombre)
        {
            listaProductos = conexion.ArticuloVenta
                .Where(producto => producto.EsPlatillo == false)
                .Where(producto => producto.Nombre.Contains(nombre))
                .Include(producto => producto.Producto)
                .ToList();

            return listaProductos;
        }

        public List<ArticuloVenta> ObtenerProductosPorCodigo(string codigo)
        {
            listaProductos = conexion.ArticuloVenta
                .Where(producto => producto.EsPlatillo == false)
                .Where(producto => producto.CodigoBarra.Contains(codigo))
                .Include(producto => producto.Producto)
                .ToList();

            return listaProductos;
        }

        public Producto ObtenerProducto(string codigoBarra)
        {
            Producto articuloProducto;

            try
            {
                articuloProducto = conexion.Producto
                    .Where(producto => producto.CodigoBarra.Equals(codigoBarra))
                    .Where(producto => producto.ArticuloVenta.EsPlatillo == false)
                    .Include(producto => producto.ArticuloVenta)
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

            return articuloProducto;
        }

        public List<Consume> ConsumePlatillo(string codigoBarra)
        {
            List<Consume> consume;

            try
            {
                consume = conexion.Consume.Where(c => c.PlatilloCodigoBarra == codigoBarra).ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            return consume;
        }

        public bool RegistrarArticulo(ArticuloVenta articuloProducto)
        {
            try
            {
                conexion.Entry(articuloProducto).State = EntityState.Added;
                resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            if (resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }

        public bool ActualizarArticulo(ArticuloVenta articuloProducto)
        {
            conexion = new PizzasBDEntities();

            try
            {
                conexion.Entry(articuloProducto).State = EntityState.Modified;
                resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                resultado = SIN_CAMBIOS;
            }
            if (resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }

        public bool ActualizarCantidad(Producto articuloProducto)
        {
            try
            {
                conexion.Entry(articuloProducto).State = EntityState.Modified;
                resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            if (resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }
    }
}
