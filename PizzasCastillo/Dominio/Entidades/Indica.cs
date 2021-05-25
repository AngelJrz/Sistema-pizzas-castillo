using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class Indica
    {
        public decimal Cantidad { get; set; }
        public Producto Producto { get; set; }
        public static Indica Clone(AccesoADatos.Indica indica)
        {
            return new Indica
            {
                Cantidad = indica.Cantidad,
                Producto = Producto.Clone(indica.Producto)
            };
        }
        public static List<Indica> CloneList(List<AccesoADatos.Indica> indica)
        {
            List<Indica> list = new List<Indica>();
            indica.ToList().ForEach(i => list.Add(
                new Indica
                {
                    Cantidad = i.Cantidad,
                    Producto = Producto.Clone(i.Producto)
                }
                ));
            return list;
        }

        public static List<AccesoADatos.Indica> CloneListDA(List<Indica> indica)
        {
            List<AccesoADatos.Indica> list = new List<AccesoADatos.Indica>();
            indica.ToList().ForEach(i => list.Add(
                new AccesoADatos.Indica
                {
                    Cantidad = i.Cantidad,
                    CodigoBarra = i.Producto.CodigoBarra
                }
                ));
            return list;
        }
    }
}