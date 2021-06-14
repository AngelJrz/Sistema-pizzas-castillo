using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MPR_04
{
    [TestClass]
    public class GenerarReporteInventarioUITest
    {
        public GenerarReporteInventarioUITest()
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

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var registrarProductoBoton = sesion.FindElementByAccessibilityId("GenerarReporte");
            registrarProductoBoton.Click();
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
        public void GenerarPDFParcialGUI()
        {
            var generarBoton = sesion.FindElementByAccessibilityId("GenerarBoton");
            generarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var nombrree = sesion.FindElementByClassName("Edit");
            nombrree.SendKeys("PRUEBAPDFPARCIAL");
            var guardarBoton = sesion.FindElementByName("Save");
            guardarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se generó el PDF de reporte de inventario.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GenerarPDFCompletoGUI()
        {
            var agregarCantidad = sesion.FindElementByAccessibilityId("PLA-000005");
            agregarCantidad.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var cantidad = sesion.FindElementByAccessibilityId("respuestaText");
            cantidad.SendKeys("10");
            var OkBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OkBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var agregarComentario = sesion.FindElementByAccessibilityId("Peperoni");
            agregarComentario.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var comentario = sesion.FindElementByAccessibilityId("respuestaText");
            comentario.SendKeys("Comentario Prueba");
            var OKBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OKBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var generarBoton = sesion.FindElementByAccessibilityId("GenerarBoton");
            generarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var nombrree = sesion.FindElementByClassName("Edit");
            nombrree.SendKeys("PRUEBAPDFCOMPLETO");
            var guardarBoton = sesion.FindElementByName("Save");
            guardarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se generó el PDF de reporte de inventario.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GuardarReporteValidoGUI()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("REPORTEAUTO");
            var agregarCantidad = sesion.FindElementByAccessibilityId("PLA-000005");
            agregarCantidad.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var cantidad = sesion.FindElementByAccessibilityId("respuestaText");
            cantidad.SendKeys("10");
            var OkBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OkBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var agregarComentario = sesion.FindElementByAccessibilityId("Peperoni");
            agregarComentario.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var comentario = sesion.FindElementByAccessibilityId("respuestaText");
            comentario.SendKeys("Comentario Prueba");
            var OKBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OKBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var generarBoton = sesion.FindElementByAccessibilityId("GuardarBoton");
            generarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se registró el reporte";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void CancelarGeneracionGUI()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("REPORTEAUTO");
            var generarBoton = sesion.FindElementByAccessibilityId("CancelarBoton");
            generarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var registrarProductoBoton = sesion.FindElementByAccessibilityId("RegistrarProductoNuevo");
            bool seCancelo = false;
            if (registrarProductoBoton != null)
                seCancelo = true;
            Assert.IsTrue(seCancelo);
        }

        [TestMethod]
        public void GuardarReporteInvalidoGUI()
        {
            var agregarCantidad = sesion.FindElementByAccessibilityId("PLA-000005");
            agregarCantidad.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var cantidad = sesion.FindElementByAccessibilityId("respuestaText");
            cantidad.SendKeys("15");
            var OkBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OkBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var agregarComentario = sesion.FindElementByAccessibilityId("Peperoni");
            agregarComentario.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var comentario = sesion.FindElementByAccessibilityId("respuestaText");
            comentario.SendKeys("Comentario Prueba");
            var OKBoton = sesion.FindElementByAccessibilityId("BotonOK");
            OKBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var generarBoton = sesion.FindElementByAccessibilityId("GuardarBoton");
            generarBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Hay campos incorrectos: Nombre, por favor verifique la información.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
