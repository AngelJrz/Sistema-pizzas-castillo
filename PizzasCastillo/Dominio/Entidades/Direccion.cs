using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string NumeroInterior { get; set; }
        public string Referencias { get; set; }
        public string NumeroExterior { get; set; }
        public string EntidadFederativa { get; set; }
        public string Colonia { get; set; }
    }
}
