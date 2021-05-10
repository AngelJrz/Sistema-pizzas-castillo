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
        public Tipo Estatus { get; set; }
        public List<Solicita> Solicita { get; set; }

        public static PedidoAProveedor Clone(AccesoADatos.PedidoAProveedor pedidoProveedor)
        {
            return new PedidoAProveedor
            {
                Id = pedidoProveedor.Id,
                Descripcion = pedidoProveedor.Descripcion,
                Fecha = pedidoProveedor.Fecha,
                CostoTotal = pedidoProveedor.CostoTotal,
                Proveedor = Proveedor.Clone(pedidoProveedor.Proveedor),
                Estatus = Tipo.Clone(pedidoProveedor.EstatusPedidoAProveedor),
                Solicita = Entidades.Solicita.CloneList(pedidoProveedor.Solicita.ToList())
            };
        }
    }
}
