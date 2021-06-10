using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;
using System.Windows.Media.Imaging;
using System.IO;
using Dominio.Utilerias;

namespace Dominio.Test.CU_MPR_01
{
    /// <summary>
    /// Summary description for GestionarProductoTest
    /// </summary>
    [TestClass]
    public class GestionarProductoTest
    {
        private ProductoController productoController;

        public GestionarProductoTest()
        {
            productoController = new ProductoController();
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
        public void GetListaProductosActivos()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Assert.IsTrue(listaProductos.Count > 0);
        }

        [TestMethod]
        public void GetListaProductos()
        {
            List<Producto> listaProductos = productoController.ObtenerProductos();

            Assert.IsTrue(listaProductos.Count > 0);
        }

        [TestMethod]
        public void GetProductosConCodigoFiltroValido()
        {
            string filtro = "PRUE";
            List<Producto> listaProductos = productoController.BuscarProductosPorCodigo(filtro);

            List<Producto> listaAuxiliar = listaProductos.FindAll(p => p.CodigoBarra.Contains(filtro));

            bool seFiltro = false;

            if (listaProductos.Count == listaAuxiliar.Count)
                seFiltro = true;

            Assert.IsTrue(seFiltro);
        }

        [TestMethod]
        public void GetProductosConCodigoFiltroInvalido()
        {
            string filtro = "QQQQQQQQQQ";
            List<Producto> listaProductos = productoController.BuscarProductosPorCodigo(filtro);

            Assert.IsFalse(listaProductos.Count > 0);
        }

        [TestMethod]
        public void GetProductosConNombreFiltroValido()
        {
            string filtro = "500 ml";
            List<Producto> listaProductos = productoController.BuscarProductosPorNombre(filtro);

            List<Producto> listaAuxiliar = listaProductos.FindAll(p => p.CodigoBarra.Contains(filtro));

            bool seFiltro = false;

            if (listaProductos.Count == listaAuxiliar.Count)
                seFiltro = true;

            Assert.IsTrue(seFiltro);
        }

        [TestMethod]
        public void GetProductosConNombreFiltroInvalido()
        {
            string filtro = "nombreFalso";
            List<Producto> listaProductos = productoController.BuscarProductosPorNombre(filtro);

            Assert.IsFalse(listaProductos.Count > 0);
        }

        [TestMethod]
        public void GetDatosProducto()
        {
            List<Producto> listaProductos = productoController.ObtenerProductos();
            Producto productoObtenido = new Producto();
            productoObtenido = listaProductos.Find(p => p.EsPlatillo == false);

            ValidadorArticuloVenta validadorArticulo = new ValidadorArticuloVenta();
            ValidadorProducto validadorProducto = new ValidadorProducto();

            bool esCorrecto = validadorArticulo.Validar(productoObtenido) && validadorProducto.Validar(productoObtenido);

            Assert.IsTrue(esCorrecto);
        }

        [TestMethod]
        public void EliminarProductoActivo()
        {
            const int DISPONIBLE = 1;

            List<Producto> listaProductos = productoController.ObtenerProductos();
            Producto productoObtenido = new Producto();
            productoObtenido = listaProductos.Find(p => p.Estatus == DISPONIBLE);

            ResultadoEliminarProducto resultado = productoController.EliminarProducto(productoObtenido);
            bool seElimino = false;

            if(resultado == ResultadoEliminarProducto.BajaExitosa)
            {
                seElimino = true;
            }

            Assert.IsTrue(seElimino);
        }

        [TestMethod]
        public void EliminarProductoNoActivo()
        {
            const int NO_DISPONIBLE = 2;

            List<Producto> listaProductos = productoController.ObtenerProductos();
            Producto productoObtenido = new Producto();
            productoObtenido = listaProductos.Find(p => p.Estatus == NO_DISPONIBLE);

            ResultadoEliminarProducto resultado = productoController.EliminarProducto(productoObtenido);
            bool seElimino = true;

            if (resultado == ResultadoEliminarProducto.BajaExitosa)
            {
                seElimino = false;
            }

            Assert.IsFalse(seElimino);
        }
    }
}
