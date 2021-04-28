using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ReporteCaja
    {
        public int Id { get; set; }
        public decimal BalanceDiario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalEntrada { get; set; }
        public decimal TotalSalida { get; set; }
        public decimal TotalEfectivoContado { get; set; }
        public string Observaciones { get; set; }
        public decimal EfectivoDiaSiguiente { get; set; }
        public Empleado GeneradoPor { get; set; }
        public List<Guarda> Guarda { get; set; }
    }
}
