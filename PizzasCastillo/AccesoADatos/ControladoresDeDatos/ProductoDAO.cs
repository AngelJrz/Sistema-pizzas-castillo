using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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

        public List<ArticuloVenta> ObtenerProductos()
        {
            listaProductos = conexion.ArticuloVenta
                .Where(producto => producto.EsPlatillo == false)
                .ToList();

            return listaProductos;
        }

        public ArticuloVenta ObtenerProducto(string codigoBarra)
        {
            ArticuloVenta articuloProducto;

            try
            {
                articuloProducto = conexion.ArticuloVenta
                    .Where(producto => producto.Username.Equals(username))
                    .Where(empleado => empleado.Persona.Estatus == 1)
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
    }
}
