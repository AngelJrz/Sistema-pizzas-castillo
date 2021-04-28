using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Merma
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Justificacion { get; set; }
        public List<Indica> Indica { get; set; }
        public Pedido Pedido { get; set; }
    }
}
