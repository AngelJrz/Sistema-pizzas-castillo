using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Entidades;
using Dominio.Logica;
using System.Collections.Generic;

namespace Dominio.Test.CU_MF_07
{
    [TestClass]
    public class ConfirmarEntregaDePedidoTest
    {
        private PedidoAProveedorController _pedidoController;

        public ConfirmarEntregaDePedidoTest()
        {
            _pedidoController = new PedidoAProveedorController();
        }

        [TestMethod]
        public void ObtenerPedidosAProveedores()
        {
            List<PedidoAProveedor> resultado = _pedidoController.ObtenerPedidosAProveedores();

            Assert.IsTrue(resultado.Count >= 0);
        }

        [TestMethod]
        public void CancelarPedidoProveedor()
        {
            PedidoAProveedor pedidoACancelar = new PedidoAProveedor()
            {
                Id = 4
            };

            bool resultado = _pedidoController.CancelarPedidoAProveedor(pedidoACancelar.Id);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void CancelarPedidoProveedor_idIncorrecto()
        {
            PedidoAProveedor pedidoACancelar = new PedidoAProveedor()
            {
                Id = 0
            };

            bool resultado = _pedidoController.CancelarPedidoAProveedor(pedidoACancelar.Id);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EntregarPedidoProveedor()
        {
            PedidoAProveedor pedido = new PedidoAProveedor()
            {
                Id = 4
            };

            bool resultado = _pedidoController.PedidoAProveedorEntregado(pedido.Id);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EntregarPedidoProveedor_idIncorrecto()
        {
            PedidoAProveedor pedidoAEntregar = new PedidoAProveedor()
            {
                Id = 0
            };

            bool resultado = _pedidoController.PedidoAProveedorEntregado(pedidoAEntregar.Id);

            Assert.IsFalse(resultado);
        }
    }
}
