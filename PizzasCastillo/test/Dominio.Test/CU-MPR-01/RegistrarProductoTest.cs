using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;
using System.Windows.Media.Imaging;
using System.IO;

namespace Dominio.Test.CU_MPR_01
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RegistrarProductoTest
    {
        private ProductoController productoController;

        public RegistrarProductoTest()
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
        public void RegistrarProductoInsumo()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path+ "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Insumo"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE100001",
                Nombre = "Lechuga Romana",
                Precio = new decimal (19.99),
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal (25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar este producto",
                PrecioCompra = new decimal (12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. No para su venta individual",
                Tipo = tipoProducto,
                UnidadDeMedida = "kilogramos"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsTrue(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoFinal()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE100002",
                Nombre = "Pepsi Cola 500ml",
                Precio = new decimal(19.99),
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar este producto",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsTrue(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoInsumoCodigoAuto()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Insumo"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = null,
                Nombre = "Tomate Italiano",
                Precio = new decimal(19.99),
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar este producto",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. No para su venta individual  ",
                Tipo = tipoProducto,
                UnidadDeMedida = "kilogramos"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsTrue(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoFinalCodigoAuto()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = null,
                Nombre = "Doritos 200g",
                Precio = new decimal(19.99),
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe almacenar en un lugar seco",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsTrue(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoSinPrecio()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Insumo"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE90009",
                Nombre = "Queso Mozzarella Blanco",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "kilogramo"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsTrue(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoFinalSinPrecio()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE80008",
                Nombre = "Queso Mozzarella Amarillo",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "kilogramo"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.RegistroFallido)
            {
                fueRegistrado = false;
            }

            Assert.IsFalse(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoSinFoto()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE10009",
                Nombre = "Queso Mozzarella Blanco",
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena. Se debe refrigerar",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = true;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = false;
            }

            Assert.IsFalse(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoDescripcionInvalida()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE300001",
                Nombre = "Producto Invalido",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena largo.Se debe almacenar en un lugar seco Descripcion de prueba con texto en cadena largo.Se debe almacenar en un lugar seco  Descripcion de prueba con texto en cadena largo.Se debe almacenar en un lugar seco.",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = false;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = true;
            }

            Assert.IsFalse(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoRestriccionInvalida()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE300003",
                Nombre = "Producto Invalido",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena.",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo. Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueRegistrado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueRegistrado = false;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueRegistrado = true;
            }

            Assert.IsFalse(fueRegistrado);
        }

        [TestMethod]
        public void RegistrarProductoTipoNulo()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE300003",
                Nombre = "Producto Invalido",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena.",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo. Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueError = false;

            try
            {
                resultado = productoController.GuardarProducto(productoPrueba);
            }
            catch (NullReferenceException)
            {
                fueError = true;
            }

            Assert.IsTrue(fueError);
        }

        [TestMethod]
        public void RegistrarProductoCodigoDuplicado()
        {
            TipoProductoController tipoProductoController = new TipoProductoController();

            byte[] foto;
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            BitmapImage bitmapImage = new BitmapImage(new Uri(path + "/CU-MPR-01/ImagenPrueba.png", UriKind.Relative));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                foto = ms.ToArray();
            }

            List<Tipo> tiposProducto = tipoProductoController.ObtenerTipoProducto();
            Tipo tipoProducto = tiposProducto.Find(tipo => tipo.Nombre.Equals("Final"));

            Producto productoPrueba = new Producto
            {
                CodigoBarra = "PRUE100001",
                Nombre = "Producto Duplicado",
                Foto = foto,
                Estatus = 1,
                EsPlatillo = false,
                NombreFoto = "ImagenPrueba",
                Cantidad = new decimal(25.5),
                Descripcion = "Descripcion de prueba con texto en cadena largo.Se debe almacenar en un lugar seco.",
                PrecioCompra = new decimal(12.50),
                Restricciones = "Restricciones de prueba con texto en cadena. Deben ser vendidos junto con un platillo ",
                Tipo = tipoProducto,
                UnidadDeMedida = "unidad"
            };

            ResultadoRegistroProducto resultado;
            bool fueDuplicado = false;

            resultado = productoController.GuardarProducto(productoPrueba);

            if (resultado == ResultadoRegistroProducto.RegistroExitoso)
            {
                fueDuplicado = false;
            }
            else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
            {
                fueDuplicado = true;
            }

            Assert.IsTrue(fueDuplicado);
        }
    }
}
