using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enumeraciones
{
    public class Efectivo
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }

        public static Efectivo Clone(AccesoADatos.Efectivo tipo)
        {
            return new Efectivo
            {
                Id = tipo.Id,
                Valor = tipo.Valor,
                Tipo = tipo.Tipo
            };
        }
    }
}
