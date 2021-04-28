using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Contiene
    {
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public ArticuloVenta ArticuloVenta { get; set; }
    }
}
