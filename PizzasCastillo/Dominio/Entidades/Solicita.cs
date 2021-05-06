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

        public static Solicita Clone(AccesoADatos.Solicita solicita)
        {
            return new Solicita
            {
                Cantidad = solicita.Cantidad,
                Precio = solicita.Precio,
                ProductoSolicitado = Producto.Clone(solicita.Producto)
            };
        }
        public static List<Solicita> CloneList(List<AccesoADatos.Solicita> solicitados)
        {
            List<Solicita> list = new List<Solicita>();
            solicitados.ToList().ForEach(solicita => list.Add(
                new Solicita
                {
                     Cantidad = solicita.Cantidad,
                    Precio = solicita.Precio,
                    ProductoSolicitado = Producto.Clone(solicita.Producto),
                }
            ));
            return list;
        }
    }
}
