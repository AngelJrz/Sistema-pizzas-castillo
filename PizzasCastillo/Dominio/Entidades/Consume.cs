using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Consume
    {
        public Producto Producto { get; set; }
        public decimal Cantidad { get; set; }

        public static Consume Clone(AccesoADatos.Consume consume)
        {
            return new Consume
            {
                Producto = Producto.Clone(consume.Producto),
                Cantidad = consume.Cantidad
            };
        }
    }
}
