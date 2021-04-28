using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ReporteInventario
    {
        public int IdReporte { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public Empleado GeneradoPor { get; set; }
        public virtual List<Reporta> Reporta { get; set; }
    }
}
