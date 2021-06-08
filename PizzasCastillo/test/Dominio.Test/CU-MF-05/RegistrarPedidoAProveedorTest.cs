using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;

namespace Dominio.Test.CU_MF_05
{
    [TestClass]
    public class RegistrarPedidoAProveedorTest
    {
        private PedidoAProveedorController _pedidoController;

        public RegistrarPedidoAProveedorTest()
        {
            _pedidoController = new PedidoAProveedorController();
        }

        [TestMethod]
        public void RegistrarPedidoAProveedor()
        {
            Producto carne = new Producto()
            {
                CodigoBarra = "CAR-527",
                Cantidad = 3,
                Precio = new decimal(150)
            };

            Solicita solicitud = new Solicita()
            {
                Cantidad = 3,
                Precio = new decimal(450),
                ProductoSolicitado = carne
            };

            List<Solicita> solicitudes = new List<Solicita>()
            {
                solicitud
            };

            PedidoAProveedor nuevoPedido = new PedidoAProveedor()
            {
                Descripcion = "Pedido de carne",
                Fecha = DateTime.Now,
                CostoTotal = new Decimal(500),
                Proveedor = new Proveedor()
                {
                    Id = 1
                },
                Estatus = new Tipo()
                {
                    Id = 1
                },
                Solicita = solicitudes
            };
            
            bool resultado = _pedidoController.RegistrarNuevoPedidoAProveedor(nuevoPedido);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void RegistrarPedidoAProveedor_ProveedorInvalido()
        {
            Producto carne = new Producto()
            {
                CodigoBarra = "CAR-527",
                Cantidad = 3,
                Precio = new decimal(150)
            };

            Solicita solicitud = new Solicita()
            {
                Cantidad = 3,
                Precio = new decimal(450),
                ProductoSolicitado = carne
            };

            List<Solicita> solicitudes = new List<Solicita>()
            {
                solicitud
            };

            PedidoAProveedor nuevoPedido = new PedidoAProveedor()
            {
                Descripcion = "Pedido de carne",
                Fecha = DateTime.Now,
                CostoTotal = new Decimal(500),
                Proveedor = new Proveedor()
                {
                    Id = 132
                },
                Estatus = new Tipo()
                {
                    Id = 1
                },
                Solicita = solicitudes
            };

            bool resultado = _pedidoController.RegistrarNuevoPedidoAProveedor(nuevoPedido);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ObtenerPedidosAProveedores()
        {
            List<PedidoAProveedor> resultado = _pedidoController.ObtenerPedidosAProveedores();

            Assert.IsTrue(resultado.Count >= 0);
        }

    }
}
