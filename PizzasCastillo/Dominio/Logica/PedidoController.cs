using AccesoADatos.ControladoresDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Entidades.Contiene;
namespace Dominio.Logica
{
    public class PedidoController
    {
        List<AccesoADatos.Contiene> listacontiene;

        public void AgregarPedido(Dominio.Entidades.Pedido pedido)
        {

            PedidosDAO pedidosDAO = new PedidosDAO();

            int idPedido = pedidosDAO.RegistrarPedido(CloneRegister(pedido));
            ProductosContienePedidoDAO contienePedidoDAO = new ProductosContienePedidoDAO();


            foreach (Dominio.Entidades.Contiene pedidocontiene in pedido.Contiene)
            {
                listacontiene.Add(CloneRegisterContiene(pedidocontiene,idPedido));



            }
            contienePedidoDAO.RegistrarProductosPedido(listacontiene);


        }

        private AccesoADatos.Pedido CloneRegister(Dominio.Entidades.Pedido pedido)
        {
            return new AccesoADatos.Pedido
            {

                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                IdPersona = pedido.SolicitadoPor.Id,
                IdEstatusPedido = pedido.Estatus.Id,
                NumeroEmpleado = pedido.RegistradoPor.Id.ToString(),
                IdTipoPedido = pedido.Tipo.Id,
                IdMesa = pedido.Mesa.Id,
                IdRepartidor=pedido.RepartidoPor.Id
               

            };
        }

        private AccesoADatos.Contiene CloneRegisterContiene(Dominio.Entidades.Contiene contiene, int id)
        {
            return new AccesoADatos.Contiene
            {
                IdPedido = id,
                CodigoBarra = contiene.ArticuloVenta.CodigoBarra.ToString(),
                Cantidad = contiene.Cantidad,
                Total = contiene.Total







            };

        }
    }
    }
