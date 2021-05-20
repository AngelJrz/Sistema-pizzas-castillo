using AccesoADatos.Excepciones;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ReporteInventarioDAO
    {
        private PizzasBDEntities conexion;
        private List<Reporta> listaProductos;
        private const int CREADO = 1;
        private const int SIN_CAMBIOS = 0;
        private int resultado;

        public ReporteInventarioDAO()
        {
            conexion = new PizzasBDEntities();
            resultado = 0;
        }

        public List<Reporta> ObtenerListaReportados(ReporteInventario reporte)
        {
            listaProductos = conexion.Reporta
                .Where(producto => producto.IdReporte == reporte.IdReporte)
                .ToList();

            return listaProductos;
        }

        public Producto ObtenerProductoReportado(string codigoBarra)
        {
            Producto articuloProducto;

            try
            {
                articuloProducto = conexion.Producto
                    .Where(producto => producto.CodigoBarra.Equals(codigoBarra))
                    .Where(producto => producto.ArticuloVenta.EsPlatillo == false)
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

        public List<ReporteInventario> ObtenerListaReportes()
        {
            throw new NotImplementedException();
        }

        public bool RegistrarReporte(ReporteInventario reporteInventario)
        {
            try
            {
                conexion.Entry(reporteInventario).State = EntityState.Added;
                resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }

            if (resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }

    }
}
