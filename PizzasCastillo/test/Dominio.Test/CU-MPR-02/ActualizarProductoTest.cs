using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;
using System.Windows.Media.Imaging;
using System.IO;

namespace Dominio.Test.CU_MPR_02
{
    /// <summary>
    /// Summary description for ActualizarProductoTest
    /// </summary>
    [TestClass]
    public class ActualizarProductoTest
    {
        private ProductoController productoController;

        public ActualizarProductoTest()
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
        public void ActualizarProductoCodigoDuplicado()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.CodigoBarra = "PRUE100001";

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarInsumoAFinal()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            TipoProductoController tipoProductoController = new TipoProductoController();
            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoCambiado = listaProductos.Find(producto => producto.Tipo.Nombre.Equals("Insumo"));

            productoCambiado.Tipo = tipoProducto;

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ActualizarFinalAInsumo()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            TipoProductoController tipoProductoController = new TipoProductoController();
            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Insumo"));

            Producto productoCambiado = listaProductos.Find(producto => producto.Tipo.Nombre.Equals("Final"));

            productoCambiado.Tipo = tipoProducto;

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ActualizarCodigoBarra()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.CodigoBarra = "PRUE777777";

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarFoto()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-02/ImagenPrueba2.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            productoCambiado.Foto = foto;
            productoCambiado.NombreFoto = "ImagenPrueba2";

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ActualizarCantidad()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.Cantidad = new decimal(0.00);
            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarEstatus()
        {
            const int NO_DISPONIBLE = 2;

            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.Estatus = NO_DISPONIBLE;
            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarNombreInvalido()
        {

            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.Nombre = "Queso Super Especial Mezcla de Cheddar, Mozzarella y GoudaNombre tipo string largo. Queso Super Especial Mezcla de Cheddar, Mozzarella y GoudaNombre tipo string largo.";
            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarDescripcionAVacio()
        {

            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.Descripcion = "";
            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActualizarDatosValidos()
        {

            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.Nombre = "Nombre Cambiado";
            productoCambiado.Precio = new decimal(9.10);
            productoCambiado.Descripcion = "Descripcion Cambiada";
            productoCambiado.PrecioCompra = new decimal(1.00);
            productoCambiado.Restricciones = "Restricciones Cambiadas";
            productoCambiado.UnidadDeMedida = "Unidades";

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ActualizarAPlatillo()
        {

            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            Producto productoCambiado = listaProductos.Find(producto => producto.CodigoBarra.Equals("PRUE100002"));

            productoCambiado.EsPlatillo = true;

            bool resultado;

            resultado = productoController.ActualizarProducto(productoCambiado);

            Assert.IsFalse(resultado);
        }
    }
}
