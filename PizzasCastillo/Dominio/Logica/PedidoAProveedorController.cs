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
        public bool RegistrarNuevoPedidoAProveedor(PedidoAProveedor nuevoPedido)
        {
            PedidoAProveedorDAO dao = new PedidoAProveedorDAO();
            bool registrado = dao.RegistrarPedidoAProveedor(CloneDominioADatos(nuevoPedido));
            return registrado;
        }

        public AccesoADatos.PedidoAProveedor CloneDominioADatos(PedidoAProveedor pedidoAClonar)
        {
            return new AccesoADatos.PedidoAProveedor()
            {
                Descripcion = pedidoAClonar.Descripcion,
                Fecha = pedidoAClonar.Fecha,
                CostoTotal = pedidoAClonar.CostoTotal,
                IdProveedor = pedidoAClonar.Proveedor.Id,
                IdEstatusPedidoAProveedor = pedidoAClonar.Estatus.Id,
                Solicita = CloneDominioADatosSolicita(pedidoAClonar.Solicita)
            };
        }

        public List<AccesoADatos.Solicita> CloneDominioADatosSolicita(List<Solicita> listaDeSolicitud)
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
