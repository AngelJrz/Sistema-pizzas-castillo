using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class PedidoAProveedor
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal CostoTotal { get; set; }
        public Proveedor Proveedor { get; set; }
        public EstatusPedidoAProveedor Estatus { get; set; }
        public List<Solicita> Solicita { get; set; }
    }
}
