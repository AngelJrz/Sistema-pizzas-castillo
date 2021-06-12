using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MPR_02
{
    [TestClass]
    public class ActualizarProductoUITest
    {
        public ActualizarProductoUITest()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;
        protected static WindowsDriver<WindowsElement> sesion;
        protected const string DRIVER_URL = "http://127.0.0.1:4723";
        private const string APP_ID = @"C:\Users\parke\Desktop\SPP\Sistema-pizzas-castillo\PizzasCastillo\Presentacion\bin\Debug\Presentacion.exe";

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
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

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            if (sesion == null)
            {
                AppiumOptions appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", APP_ID);
                appiumOptions.AddAdditionalCapability("diviceName", "WindowsPC");
                sesion = new WindowsDriver<WindowsElement>(new Uri(DRIVER_URL), appiumOptions);
            }
        }


        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("frews2");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("eladmin");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var seccionUsuarios = sesion.FindElementByAccessibilityId("Productos");
            seccionUsuarios.Click();
        }

        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            //if (sesion != null)
            //{
            //    sesion.Close();
            //    sesion.Quit();
            //}
        }
        //
        #endregion

        [TestMethod]
        public void ActualizarProductoInsumoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("FINGOU-146");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var tipoProducto = sesion.FindElementByAccessibilityId("ListaTiposProducto");
            tipoProducto.SendKeys("Final");
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Lechuga Actualizada Prueba");
            var precioVenta = sesion.FindElementByAccessibilityId("precioVentaText");
            precioVenta.Clear();
            precioVenta.SendKeys("25.50");
            var precioUnitario = sesion.FindElementByAccessibilityId("precioUnitarioText");
            precioUnitario.Clear();
            precioUnitario.SendKeys("10.00");
            var unidadMedida = sesion.FindElementByAccessibilityId("unidadMedidaText");
            unidadMedida.Clear();
            unidadMedida.SendKeys("unidad");
            var descripcion = sesion.FindElementByAccessibilityId("descripcionText");
            descripcion.Clear();
            descripcion.SendKeys("Descripcion Nueva.");
            var restricciones = sesion.FindElementByAccessibilityId("restriccionesText");
            restricciones.Clear();
            restricciones.SendKeys("Restricciones Nuevas.");
            var botonImagen = sesion.FindElementByAccessibilityId("agregarFotoBoton");
            botonImagen.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var imagenes = sesion.FindElementByName("Pictures");
            imagenes.Click();
            var imagenSeleccionada = sesion.FindElementByName("Liberado.png");
            imagenSeleccionada.Click();
            var abrirBoton = sesion.FindElementByName("Open");
            abrirBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonRegistrar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonRegistrar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se actualizó el producto";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void ActualizarProductoSinCambiarFotoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("FINGOU-146");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var tipoProducto = sesion.FindElementByAccessibilityId("ListaTiposProducto");
            tipoProducto.SendKeys("Final");
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Lechuga Romana Nueva");
            var precioVenta = sesion.FindElementByAccessibilityId("precioVentaText");
            precioVenta.Clear();
            precioVenta.SendKeys("10.50");
            var precioUnitario = sesion.FindElementByAccessibilityId("precioUnitarioText");
            precioUnitario.Clear();
            precioUnitario.SendKeys("8.00");
            var unidadMedida = sesion.FindElementByAccessibilityId("unidadMedidaText");
            unidadMedida.Clear();
            unidadMedida.SendKeys("kilogramo");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var botonRegistrar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonRegistrar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se actualizó el producto";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void CancelarActualizarGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("FINGOU-146");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var cancelarBoton = sesion.FindElementByAccessibilityId("CancelarBoton");
            cancelarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var registrarProductoBotonNuevo = sesion.FindElementByAccessibilityId("RegistrarProductoNuevo");
            bool seCancelo = false;
            if (registrarProductoBoton != null)
                seCancelo = true;
            Assert.IsTrue(seCancelo);
        }

        [TestMethod]
        public void CancelarCambiarFotoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("FINGOU-146");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonImagen = sesion.FindElementByAccessibilityId("agregarFotoBoton");
            botonImagen.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var imagenes = sesion.FindElementByName("Pictures");
            imagenes.Click();
            var imagenSeleccionada = sesion.FindElementByName("Evaluado.png");
            imagenSeleccionada.Click();
            var cancelBoton = sesion.FindElementByName("Cancel");
            cancelBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var tipoProducto = sesion.FindElementByAccessibilityId("ListaTiposProducto");
            bool seCancelo = false;
            if (tipoProducto != null)
                seCancelo = true;
            Assert.IsTrue(seCancelo);
        }

        [TestMethod]
        public void ActualizarProductoInvalidoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("FINGOU-146");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var tipoProducto = sesion.FindElementByAccessibilityId("ListaTiposProducto");
            tipoProducto.SendKeys("Final");
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys(" ");
            var precioVenta = sesion.FindElementByAccessibilityId("precioVentaText");
            precioVenta.Clear();
            var unidadMedida = sesion.FindElementByAccessibilityId("unidadMedidaText");
            unidadMedida.Clear();
            unidadMedida.SendKeys("kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo kilogramo ");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var botonRegistrar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonRegistrar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Los siguientes campos están incorrectos: por favor verifique la información.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
