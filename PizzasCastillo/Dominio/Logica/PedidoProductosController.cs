using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class PedidoProductosController
    {
        decimal totalPedido = 0;
        public void RegistrarPedido(List<AccesoADatos.ArticuloVenta> articulos, AccesoADatos.Pedido pedido) {

            PedidosDAO pedidosDAO = new PedidosDAO();

            pedidosDAO.RegistrarPedido(pedido);

            ProductosContienePedidoDAO productosDelPedido = new ProductosContienePedidoDAO();

        }




        private void CalCularTotal(List<AccesoADatos.ArticuloVenta> articulos)
        {
            foreach (AccesoADatos.ArticuloVenta articulo in articulos)
            {
                

            }

        }


    }





   
}

