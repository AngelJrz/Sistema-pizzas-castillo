using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Producto : ArticuloVenta
    {
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public string Restricciones { get; set; }
        public TipoProducto Tipo { get; set; }
        public string UnidadDeMedida { get; set; }
    }
}
