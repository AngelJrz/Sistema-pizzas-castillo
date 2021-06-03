
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using System.IO;
using Dominio.Utilerias;

namespace Dominio.Test.CU_MPE_06
{
    /// <summary>
    /// Summary description for RegistroDePagoDePedidoTest
    /// </summary>
    [TestClass]
    public class RegistroDePagoDePedidoTest
    {
        private EmpleadoController empleadoController;
        private PedidoController pedidoController;

        public RegistroDePagoDePedidoTest()
        {
            empleadoController = new EmpleadoController();
            pedidoController = new PedidoController();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void ActualizarPedidoValido()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Encargado de caja"));
            List<Pedido> listaPedidos = pedidoController.ObtenerPedidos();
            Pedido pedidoPrueba = listaPedidos.Find(p => p.Estatus.Nombre.Equals("Entregado"));
            decimal pagoPrueba = 999;
            bool seGuardo = pedidoController.ActualizarAPagado(pedidoPrueba, pagoPrueba);
            Assert.IsTrue(seGuardo);
        }

        [TestMethod]
        public void ActualizarPedidoInvalidoPorPaga()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Encargado de caja"));
            List<Pedido> listaPedidos = pedidoController.ObtenerPedidos();
            Pedido pedidoPrueba = listaPedidos.Find(p => p.Estatus.Nombre.Equals("Entregado"));
            decimal pagoPrueba = 0;
            bool seGuardo = pedidoController.ActualizarAPagado(pedidoPrueba, pagoPrueba);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void ActualizarPedidoInvalidoPorEstatus()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Encargado de caja"));
            List<Pedido> listaPedidos = pedidoController.ObtenerPedidos();
            Pedido pedidoPrueba = listaPedidos.Find(p => p.Estatus.Nombre.Equals("Preparado"));
            decimal pagoPrueba = 0;
            bool seGuardo = pedidoController.ActualizarAPagado(pedidoPrueba, pagoPrueba);
            Assert.IsFalse(seGuardo);
        }
    }
}