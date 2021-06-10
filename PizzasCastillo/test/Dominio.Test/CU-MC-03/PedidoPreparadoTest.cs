using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Dominio.Test.CU_MC_03
{
    [TestClass]
    public class PedidoPreparadoTest
    {
        /*
        private readonly int PEDIDO_PREPARADO_ESTATUS = 2;
        [TestMethod]
        public void ActualizarPedidoAPreparado()
        {
            List<Entidades.Pedido> listaPedido = new List<Entidades.Pedido>();
            PedidoController pedidoController = new PedidoController();
            listaPedido = pedidoController.ObtenerPedidosPreparar();
            Entidades.Pedido localPedido = listaPedido[0];
            bool actualizado;
            
            try
            {
                localPedido.Estatus.Id = PEDIDO_PREPARADO_ESTATUS;
                pedidoController.ActualizarPedidoEstatus(localPedido);
                actualizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Assert.IsTrue(actualizado);
        }

        [TestMethod]
        public void ActualizarPedidoAPreparadoFalso()
        {
            PedidoController pedidoController = new PedidoController();
            Entidades.Pedido localPedido = new Entidades.Pedido();
            localPedido.Id = 0;
            bool actualizado = pedidoController.ActualizarPedidoEstatus(localPedido);
            Assert.AreEqual(actualizado,false);
            
        }
        */
    }
}
