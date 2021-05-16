using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Consume
    {
        public Platillo Platillo { get; set; }
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

        public static List<Consume> CloneList(List<AccesoADatos.Consume> productosPlatillo)
        {
            List<Consume> list = new List<Consume>();
            productosPlatillo.ToList().ForEach(consume => list.Add(
                new Consume
                {
                    Producto = Producto.Clone(consume.Producto),
                    Cantidad = consume.Cantidad
                }
            ));
            return list;
        }
    }
}
