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

        public static ReporteCaja Clone(AccesoADatos.ReporteCaja reporteCaja)
        {
            return new ReporteCaja
            {
                Id = reporteCaja.Id,
                BalanceDiario = reporteCaja.BalanceDiario,
                Fecha = reporteCaja.Fecha,
                TotalEntrada = reporteCaja.TotalEntrada,
                TotalSalida = reporteCaja.TotalSalida,
                TotalEfectivoContado = reporteCaja.TotalEfectivoContado,
                Observaciones = reporteCaja.Observaciones,
                EfectivoDiaSiguiente = reporteCaja.EfectivoDiaSiguiente,
                GeneradoPor = Empleado.Clone(reporteCaja.Empleado),
                Guarda = Entidades.Guarda.CloneList(reporteCaja.Guarda.ToList())
            };
        }
    }
}
