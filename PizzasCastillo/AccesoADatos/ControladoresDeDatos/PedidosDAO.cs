using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
   public  class PedidosDAO
    {
        private PizzasBDEntities _connection;
        private List<Pedido> _pedidos = null;
        private Pedido _pedidoEncontrado;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;

        public int RegistrarPedido(Pedido pedido)
        {
            try
            {
                _connection.Pedido.Add(pedido);
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

           

            return pedido.Id;
        }




        public bool ActualizarPedido(Pedido pedido) {

            try
            {
                Pedido pedidoEncontrado = (Pedido)_connection.Pedido.Where(p => p.Id == pedido.Id);
                pedidoEncontrado = pedido;
                _connection.Pedido.Add(pedidoEncontrado);


            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }



        public List<Pedido> ObtenerPedidos() {
            try
            {


                _pedidos = _connection.Pedido.ToList();
                return _pedidos;
            }
            catch (Exception) {
                return _pedidos;
            }
        }


        public Pedido ObtenerPedidoPorID(int id)
        {
            try
            {


                _pedidoEncontrado = _connection.Pedido.Where(x=>x.Id==id).FirstOrDefault();
                return _pedidoEncontrado;
            }
            catch (Exception)
            {
                return _pedidoEncontrado;
            }
        }


    }
}
