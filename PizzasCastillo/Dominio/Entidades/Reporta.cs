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

        public static Reporta Clone(AccesoADatos.Reporta reporta)
        {
            return new Reporta
            {
                CantidadEnInventario = reporta.CantidadEnInventario,
                CantidadReal = reporta.CantidadReal,
                Comentario = reporta.Comentario,
                Producto = Producto.Clone(reporta.Producto)
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
                    Producto = Producto.Clone(reporta.Producto)
                }
            ));
            return list;
        }
    }
}