using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class PedidoAProveedorController
    {
        private int ID_PEDIDO_ENTREGADO = 1;
        private int ID_PEDIDO_CANCELADO = 2;
        private int ID_PEDIDO_SOLICITADO = 3;

        public bool RegistrarNuevoPedidoAProveedor(PedidoAProveedor nuevoPedido)
        {
            PedidoAProveedorDAO dao = new PedidoAProveedorDAO();
            bool registrado = dao.RegistrarPedidoAProveedor(CloneDominioADatos(nuevoPedido));
            return registrado;
        }

        public List<PedidoAProveedor> ObtenerPedidosAProveedores()
        {
            List<PedidoAProveedor> listaARetornar = new List<PedidoAProveedor>();
            PedidoAProveedorDAO dao = new PedidoAProveedorDAO();
            List<AccesoADatos.PedidoAProveedor> listaDePedidosdatos = dao.obtenerPedidosAProveedor();

            foreach (AccesoADatos.PedidoAProveedor pedidoDatos in listaDePedidosdatos)
            {
                if (pedidoDatos.IdEstatusPedidoAProveedor == ID_PEDIDO_SOLICITADO)
                {
                    listaARetornar.Add(PedidoAProveedor.Clone(pedidoDatos));
                }
            }

            return listaARetornar;
        }

        public bool CancelarPedidoAProveedor(int pedido)
        {
            PedidoAProveedorDAO dao = new PedidoAProveedorDAO();
            bool cancelado = dao.ActualizarEstadoPedidoAProveedor(pedido, ID_PEDIDO_CANCELADO);
            return cancelado;
        }

        public bool PedidoAProveedorEntregado(int pedido)
        {
            PedidoAProveedorDAO dao = new PedidoAProveedorDAO();
            bool entregado = dao.ActualizarEstadoPedidoAProveedor(pedido, ID_PEDIDO_ENTREGADO);
            return entregado;
        }

        private AccesoADatos.PedidoAProveedor CloneDominioADatos(PedidoAProveedor pedidoAClonar)
        {
            return new AccesoADatos.PedidoAProveedor()
            {
                Id = pedidoAClonar.Id,
                Descripcion = pedidoAClonar.Descripcion,
                Fecha = pedidoAClonar.Fecha,
                CostoTotal = pedidoAClonar.CostoTotal,
                IdProveedor = pedidoAClonar.Proveedor.Id,
                IdEstatusPedidoAProveedor = pedidoAClonar.Estatus.Id,
                Solicita = CloneDominioADatosSolicita(pedidoAClonar.Solicita)
            };
        }

        private List<AccesoADatos.Solicita> CloneDominioADatosSolicita(List<Solicita> listaDeSolicitud)
        {
            List<AccesoADatos.Solicita> listaRetorno = new List<AccesoADatos.Solicita>();

            foreach (Solicita solicitud in listaDeSolicitud)
            {
                listaRetorno.Add(new AccesoADatos.Solicita()
                {
                    CodigoBarra = solicitud.ProductoSolicitado.CodigoBarra,
                    Cantidad = solicitud.Cantidad,
                    Precio = solicitud.Precio
                });
            }

            return listaRetorno;

        }
    }
}