using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MPR_03
{
    [TestClass]
    public class GestionarProductoUITest
    {
        public GestionarProductoUITest()
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
        public void EliminarProductoValidoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("1");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonEliminar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonEliminar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Baja exitosa.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EliminarProductoInactivoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("2");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonEliminar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonEliminar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Ya se dio de baja este producto.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EliminarProductoInvalidoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("1");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonEliminar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonEliminar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "No se puede eliminar un Producto hasta que su cantidad sea 0.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void CancelarEliminarProductoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("2");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonEliminar = sesion.FindElementByAccessibilityId("CancelarBoton");
            botonEliminar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonRegistar = sesion.FindElementByAccessibilityId("RegistrarProductoNuevo");
            bool seCancelo = false;
            if (botonRegistar != null)
                seCancelo = true;
            Assert.IsTrue(seCancelo);
        }

        [TestMethod]
        public void ConsultarDetallesProductoGUI()
        {
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("Lechuga Romana Nueva");
            registrarProductoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            var botonEliminar = sesion.FindElementByAccessibilityId("RegresarBoton");
            botonEliminar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonRegistar = sesion.FindElementByAccessibilityId("RegistrarProductoNuevo");
            bool seRegreso = false;
            if (botonRegistar != null)
                seRegreso = true;
            Assert.IsTrue(seRegreso);
        }

        [TestMethod]
        public void BuscarFiltroProductoGUI()
        {
            var filtroText = sesion.FindElementByAccessibilityId("FiltroBusqueda");
            filtroText.SendKeys("Nombre");
            var busquedaText = sesion.FindElementByAccessibilityId("BusquedaText");
            busquedaText.SendKeys("Lechuga");
            var botonBuscar = sesion.FindElementByAccessibilityId("BuscarBoton");
            botonBuscar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var producto = sesion.FindElementByAccessibilityId("PRUEGUI101");
            bool seConsulto = false;
            if (producto != null)
                seConsulto = true;
            Assert.IsTrue(seConsulto);
        }

        [TestMethod]
        public void BuscarFiltroInvalidoGUI()
        {
            var filtroText = sesion.FindElementByAccessibilityId("FiltroBusqueda");
            filtroText.SendKeys("Código de Barra");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var busquedaText = sesion.FindElementByAccessibilityId("BusquedaText");
            busquedaText.SendKeys("QQQQQ");
            var botonBuscar = sesion.FindElementByAccessibilityId("BuscarBoton");
            botonBuscar.Click();
            bool seConsulto = false;
            if (botonBuscar != null)
                seConsulto = true;
            Assert.IsTrue(seConsulto);
        }
    }
}
