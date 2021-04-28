using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Solicita
    {
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public Producto ProductoSolicitado { get; set; }
    }
}
