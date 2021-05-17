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
