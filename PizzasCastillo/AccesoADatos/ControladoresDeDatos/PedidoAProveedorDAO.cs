using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace AccesoADatos.ControladoresDeDatos
{
    public class PedidoAProveedorDAO
    {
        private PizzasBDEntities conexion;

        public PedidoAProveedorDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public bool RegistrarPedidoAProveedor(PedidoAProveedor nuevoPedido)
        {
            bool registrado = false;

            try
            {
                conexion.Entry(nuevoPedido).State = EntityState.Added;
                conexion.SaveChanges();
                registrado = true;
            }
            catch (DbUpdateException)
            {
                return registrado;
            }

            return registrado;
        }

        public List<PedidoAProveedor> obtenerPedidosAProveedor()
        {
            List<PedidoAProveedor> listaDePedidos = new List<PedidoAProveedor>();

            try
            {
                listaDePedidos = conexion.PedidoAProveedor.Include("Solicita").ToList();
            }
            catch (DbUpdateException)
            {
                
            }

            return listaDePedidos;
        }

        public bool ActualizarEstadoPedidoAProveedor(PedidoAProveedor pedidoNuevo, int idNuevoEstado)
        {
            bool actualizado = false;
            try
            {
                PedidoAProveedor pedidoDB = conexion.PedidoAProveedor.Where(pedido => pedido.Id == pedidoNuevo.Id).SingleOrDefault();
                
                pedidoDB.IdEstatusPedidoAProveedor = idNuevoEstado;
                conexion.Entry(pedidoDB).State = EntityState.Modified;
                conexion.SaveChanges();
                
                actualizado = true;
            }
            catch (DbUpdateException ex)
            {
                string mess = ex.Message;
            }

            return actualizado;
        }
    }
}
