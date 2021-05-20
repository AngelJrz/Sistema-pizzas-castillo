using Dominio.Logica;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class Reporta
    {
        public decimal CantidadEnInventario { get; set; }
        public decimal CantidadReal { get; set; }
        public string Comentario { get; set; }
        public Producto Producto { get; set; }
        public ReporteInventario Reporte { get; set; }

        public static Reporta Clone(AccesoADatos.Reporta reporta)
        {
            return new Reporta
            {
                CantidadEnInventario = reporta.CantidadEnInventario,
                CantidadReal = reporta.CantidadReal,
                Comentario = reporta.Comentario,
                Producto = Producto.Clone(reporta.Producto),
                Reporte = ReporteInventario.Clone(reporta.ReporteInventario)
            };
        }
        public static List<Reporta> CloneList(List<AccesoADatos.Reporta> reportes)
        {
            List<Reporta> list = new List<Reporta>();
            reportes.ToList().ForEach(reporta => list.Add(
                new Reporta
                {
                    CantidadEnInventario = reporta.CantidadEnInventario,
                    CantidadReal = reporta.CantidadReal,
                    Comentario = reporta.Comentario,
                    Producto = Producto.Clone(reporta.Producto),
                    Reporte = ReporteInventario.Clone(reporta.ReporteInventario)
                }
            ));
            return list;
        }

        public static List<AccesoADatos.Reporta> CloneToDBEntityList(List<Reporta> reportes)
        {
            ProductoController productoController = new ProductoController();
            List<AccesoADatos.Reporta> list = new List<AccesoADatos.Reporta>();

            reportes.ForEach(reporta => list.Add(
                new AccesoADatos.Reporta
                {
                    CantidadEnInventario = reporta.CantidadEnInventario,
                    CantidadReal = reporta.CantidadReal,
                    Comentario = reporta.Comentario,
                    CodigoBarra = reporta.Producto.CodigoBarra
                }
            ));
            return list;
        }
    }
}