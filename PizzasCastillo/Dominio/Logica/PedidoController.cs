using AccesoADatos.ControladoresDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    class PedidoController
    {


        private void EditarInfoPedido(AccesoADatos.Pedido pedidoEditar)
        {




            PedidosDAO pedidosDAO = new PedidosDAO();

            pedidosDAO.ActualizarPedido(pedidoEditar);







        }
    }
}
