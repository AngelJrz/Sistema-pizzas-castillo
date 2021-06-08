using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Contiene
    {
        public int IdPedido { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public ArticuloVenta ArticuloVenta { get; set; }

        public static Contiene CloneMermaPedido(AccesoADatos.Contiene contiene)
        {
            return new Contiene
            {
                IdPedido = contiene.IdPedido,
                Cantidad = contiene.Cantidad,
                Total = contiene.Total,
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
