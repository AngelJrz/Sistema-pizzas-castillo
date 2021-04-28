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
        public TipoGasto Tipo { get; set; }
        public Empleado RegistradoPor { get; set; }
    }
}
