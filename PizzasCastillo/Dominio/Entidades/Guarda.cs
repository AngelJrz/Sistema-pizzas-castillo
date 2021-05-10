using Dominio.Enumeraciones;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class Guarda
    {
        public int Cantidad { get; set; }
        public Efectivo Efectivo { get; set; }

        public static Guarda Clone(AccesoADatos.Guarda guarda)
        {
            return new Guarda
            {
                Cantidad = guarda.Cantidad,
                Efectivo = Efectivo.Clone(guarda.Efectivo)
            };
        }
        public static List<Guarda> CloneList(List<AccesoADatos.Guarda> dinerosGuardados)
        {
            List<Guarda> list = new List<Guarda>();
            dinerosGuardados.ToList().ForEach(guarda => list.Add(
                new Guarda
                {
                    Cantidad = guarda.Cantidad,
                    Efectivo = Efectivo.Clone(guarda.Efectivo)
                }
            ));
            return list;
        }
    }
}