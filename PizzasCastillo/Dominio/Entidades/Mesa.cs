using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Estatus { get; set; }

        public static Mesa Clone(AccesoADatos.Mesa mesa)
        {
            return new Mesa
            {
                Id = mesa.IdMesa,
                Estatus = mesa.Estatus
            };
        }


        public static List<Mesa> CloneList(List<AccesoADatos.Mesa> mesas)
        {
            List<Mesa> list = new List<Mesa>();
            mesas.ToList().ForEach(mesa => list.Add(
                new Mesa
                {
                    Id = mesa.IdMesa,
                    Estatus = mesa.Estatus
                }
            )); ;
            return list;
        }
    }
}