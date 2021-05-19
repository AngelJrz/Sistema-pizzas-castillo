using AccesoADatos.ControladoresDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Entidades.Contiene;
using static Dominio.Entidades.Pedido;
using Dominio.Entidades;
namespace Dominio.Logica
{
    public class PedidoController
    {
      

        public void AgregarPedido(Pedido pedido)
        {
            PedidosDAO dao = new PedidosDAO();

            if (pedido.Tipo.Id == 1)
            {
                dao.RegistrarPedido(CloneDominioADatosParaLlevar(pedido));
            }
            else 
            {
                dao.RegistrarPedido(CloneDominioADatosLocal(pedido));
            }


        }

         
        
        public AccesoADatos.Pedido CloneDominioADatosParaLlevar(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
               
                Fecha = pedidoAClonar .Fecha,
                Total = pedidoAClonar.Total,
                IdPersona=pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido=pedidoAClonar.Estatus.Id,
                IdTipoPedido=pedidoAClonar.Tipo.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdRepartidor = pedidoAClonar.RepartidoPor.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }


        public AccesoADatos.Pedido CloneDominioADatosLocal(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
                Fecha = pedidoAClonar.Fecha,
                Total = pedidoAClonar.Total,
                IdPersona = pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido = pedidoAClonar.Estatus.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdTipoPedido = pedidoAClonar.Tipo.Id,
                IdMesa = pedidoAClonar.Mesa.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }


        public List<AccesoADatos.Contiene> CloneDominioADatosContiene(List<Contiene> listaDeSolicitud)
        {
            List<AccesoADatos.Contiene> listaRetorno = new List<AccesoADatos.Contiene>();

            foreach (Contiene solicitud in listaDeSolicitud)
            {
                listaRetorno.Add(new AccesoADatos.Contiene()
                {
                    CodigoBarra = solicitud.ArticuloVenta.CodigoBarra,
                    Cantidad = solicitud.Cantidad,
                    Total = solicitud.Total
                });
            }

            return listaRetorno;
        }





        public List<Pedido> ObtenerPedidos() 
        {

            PedidosDAO dao = new PedidosDAO();
            List < AccesoADatos.Pedido > pedidosEncontrados = dao.ObtenerPedidosEnPreparacion();
            List<Pedido> pedidosClonados = CloneList(pedidosEncontrados);
            return pedidosClonados;
           

        
        
        }


    }
    }
