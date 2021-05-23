using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AccesoADatos.ControladoresDeDatos
{
    public class TipoProductoDAO
    {
        private PizzasBDEntities conexion;
        private List<TipoProducto> tiposProducto;

        public TipoProductoDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public List<TipoProducto> ObtenerTipos()
        {
            try
            {
                tiposProducto = conexion.TipoProducto.ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }


            return tiposProducto;
        }

        public bool RegistrarTipoProducto(TipoProducto nuevoTipo)
        {
            bool registrado = false;

            try
            {
                conexion.Entry(nuevoTipo).State = System.Data.Entity.EntityState.Added;
                conexion.SaveChanges();
                registrado = true;
            }
            catch (DbUpdateException)
            {
                return registrado;
            }

            return registrado;
        }
    }
}
