using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class DireccionProveedor
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string EntidadFederativa { get; set; }
        public string Numero { get; set; }
    }
}
