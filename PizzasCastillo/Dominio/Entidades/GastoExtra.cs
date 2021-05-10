using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class GastoExtra
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public Tipo Tipo { get; set; }
        public Empleado RegistradoPor { get; set; }
        public static GastoExtra Clone(AccesoADatos.GastoExtra gasto)
        {
            return new GastoExtra
            {
                Id = gasto.Id,
                Comentario = gasto.Comentario,
                Total = gasto.Total,
                Fecha = gasto.Fecha,
                Tipo = Tipo.Clone(gasto.TipoGasto),
                RegistradoPor = Empleado.Clone(gasto.Empleado)
            };
        }
    }
}
