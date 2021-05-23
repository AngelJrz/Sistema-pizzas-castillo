using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class ReporteInventario
    {
        public int IdReporte { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public Empleado GeneradoPor { get; set; }
        public virtual List<Reporta> Reporta { get; set; }

        public static ReporteInventario Clone(AccesoADatos.ReporteInventario reporte)
        {
            return new ReporteInventario
            {
                IdReporte = reporte.IdReporte,
                Fecha = reporte.Fecha,
                Nombre = reporte.Nombre,
                GeneradoPor = Empleado.Clone(reporte.Empleado),
                Reporta = Entidades.Reporta.CloneList(reporte.Reporta.ToList())
            };
        }

        public static AccesoADatos.ReporteInventario CloneToDBEntity(ReporteInventario reporta)
        {
            return new AccesoADatos.ReporteInventario
            {
                IdReporte = reporta.IdReporte,
                Fecha = reporta.Fecha,
                Nombre = reporta.Nombre,
                NumeroEmpleado = reporta.GeneradoPor.NumeroEmpleado,
                Reporta = Entidades.Reporta.CloneToDBEntityList(reporta.Reporta.ToList())
            };
        }
    }
}
