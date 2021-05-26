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

        public static Contiene Clone(AccesoADatos.Contiene contiene)
        {
            return new Contiene
            {
                Cantidad = contiene.Cantidad,
                Total = contiene.Total,
                ArticuloVenta = ArticuloVenta.CloneProducto(contiene.ArticuloVenta)
            };
        }

        public static List<Contiene> CloneList(List<AccesoADatos.Contiene> contiene)
        {
            List<Contiene> list = new List<Contiene>();
            contiene.ToList().ForEach(c => list.Add(
                new Contiene
                {
                    Cantidad = c.Cantidad,
                    Total = c.Total,
                    ArticuloVenta = ArticuloVenta.CloneProductoPartial(c.ArticuloVenta)
                }
                ));
            return list;
        }
    }
}
