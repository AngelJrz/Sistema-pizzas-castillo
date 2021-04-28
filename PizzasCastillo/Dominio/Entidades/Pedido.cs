using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public Persona SolicitadoPor { get; set; }
        public EstatusPedido Estatus { get; set; }
        public Empleado RegistradoPor { get; set; }
        public TipoPedido Tipo { get; set; }
        public Repartidor RepartidoPor { get; set; }
        public Mesa Mesa { get; set; }
        public List<Contiene> Contiene { get; set; }
    }
}
