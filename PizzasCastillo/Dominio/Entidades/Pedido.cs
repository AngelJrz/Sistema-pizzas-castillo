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
        public  int Id { get; set; } 
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public Persona SolicitadoPor { get; set; }
        public Tipo Estatus { get; set; }
        public Empleado RegistradoPor { get; set; }
        public Tipo Tipo { get; set; }
        public Repartidor RepartidoPor { get; set; }
        public Mesa Mesa { get; set; }
        public List<Contiene> Contiene { get; set; }

        public static Pedido CloneParaLlevar(AccesoADatos.Pedido pedido)
        {
            return new Pedido
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                SolicitadoPor = Persona.Clone(pedido.Persona),
                Estatus = Tipo.Clone(pedido.EstatusPedido),
                RegistradoPor = Empleado.Clone(pedido.Empleado),
                Tipo = Tipo.Clone(pedido.TipoPedido),
                RepartidoPor = Repartidor.Clone(pedido.Repartidor),
                Mesa = null,
                Contiene = Entidades.Contiene.CloneList(pedido.Contiene.ToList())
            };
        }
        public static Pedido Clone(AccesoADatos.Pedido pedido)
        {
            return new Pedido
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                SolicitadoPor = Persona.Clone(pedido.Persona),
                Estatus = Tipo.Clone(pedido.EstatusPedido),
                RegistradoPor = Empleado.Clone(pedido.Empleado),
                Tipo = Tipo.Clone(pedido.TipoPedido),
                RepartidoPor = Repartidor.Clone(pedido.Repartidor),
                Mesa = null,
                Contiene = Entidades.Contiene.CloneList(pedido.Contiene.ToList())
            };
        }
        public static Pedido CloneParaLocal(AccesoADatos.Pedido pedido)
        {
            return new Pedido
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                SolicitadoPor = Persona.Clone(pedido.Persona),
                Estatus = Tipo.Clone(pedido.EstatusPedido),
                RegistradoPor = Empleado.Clone(pedido.Empleado),
                Tipo = Tipo.Clone(pedido.TipoPedido),
                RepartidoPor = null,
                Mesa = Mesa.Clone(pedido.Mesa),
                Contiene = Entidades.Contiene.CloneList(pedido.Contiene.ToList())
            };

        }

        public static Pedido CloneList(AccesoADatos.Pedido pedido)
        {
            return new Pedido
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                SolicitadoPor = Persona.Clone(pedido.Persona),
                Estatus = Tipo.Clone(pedido.EstatusPedido),
                RegistradoPor = Empleado.Clone(pedido.Empleado),
                Tipo = Tipo.Clone(pedido.TipoPedido),
                RepartidoPor = Repartidor.Clone(pedido.Repartidor),
                Mesa = Mesa.Clone(pedido.Mesa),
                Contiene = Entidades.Contiene.CloneList(pedido.Contiene.ToList())
            };

        }


        public static List<Pedido> CloneListParaLevar(List<AccesoADatos.Pedido> pedidos)
        {
            List<Pedido> list = new List<Pedido>();


            pedidos.ToList().ForEach(pedido => list.Add(
              
                new Pedido
                {
                    Id = pedido.Id,
                    Fecha = pedido.Fecha,
                    Total = pedido.Total,
                    SolicitadoPor = Persona.Clone(pedido.Persona),
                    Estatus = Tipo.Clone(pedido.EstatusPedido),
                    RegistradoPor = Empleado.Clone(pedido.Empleado),
                    Tipo = Tipo.Clone(pedido.TipoPedido),
                    RepartidoPor = Repartidor.Clone(pedido.Repartidor),
                    Mesa = Mesa.Clone(pedido.Mesa),
                    Contiene = Entidades.Contiene.CloneList(pedido.Contiene.ToList())
                }
                ));
            return list;
        }





    }

}
