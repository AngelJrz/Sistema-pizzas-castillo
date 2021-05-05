using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Platillo : ArticuloVenta
    {
        public DateTime FechaRegisto { get; set; }
        public string Receta { get; set; }
        public List<Consume> Consume { get; set; }

        public static Platillo Clone(AccesoADatos.Platillo platillo)
        {
            return new Platillo
            {
                FechaRegisto = platillo.FechaRegisto,
                Receta = platillo.Receta,
                Consume = Entidades.Consume.CloneList(platillo.Consume.ToList())
            };
        }
    }
}
