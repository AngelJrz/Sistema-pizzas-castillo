using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Dominio.Test.CU_MPE_06
{
    [TestClass]
    public class RegistroDePagoDePedidoUITest
    {
        public RegistroDePagoDePedidoUITest()
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
            usuarioTextBox.SendKeys("reaperuz");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("uzi");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var seccionPedidos = sesion.FindElementByAccessibilityId("Pedidos");
            seccionPedidos.Click();
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
        public void RegistrarPagoEstatusInvalidaGUI()
        {
            var pagoPedidoBoton = sesion.FindElementByAccessibilityId("En Proceso");
            pagoPedidoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var pagoText = sesion.FindElementByAccessibilityId("PagoText");
            pagoText.SendKeys("999");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonConfirmar = sesion.FindElementByAccessibilityId("ConfirmarBoton");
            botonConfirmar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Debe ser Estatus tipo Entregado el pedido para cambiarlo a Registrado";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarPagoInvalidoGUI()
        {
            var pagoPedidoBoton = sesion.FindElementByAccessibilityId("Entregado");
            pagoPedidoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var pagoText = sesion.FindElementByAccessibilityId("PagoText");
            pagoText.SendKeys("1");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonConfirmar = sesion.FindElementByAccessibilityId("ConfirmarBoton");
            botonConfirmar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Debe ser mayor o igual al total del Pedido la Cantidad a Recibir";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarPagoValidoGUI()
        {
            var pagoPedidoBoton = sesion.FindElementByAccessibilityId("Entregado");
            pagoPedidoBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var pagoText = sesion.FindElementByAccessibilityId("PagoText");
            pagoText.SendKeys("999");
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonConfirmar = sesion.FindElementByAccessibilityId("ConfirmarBoton");
            botonConfirmar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var botonAceptar = sesion.FindElementByAccessibilityId("AceptarBoton");
            botonAceptar.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se actualizó a Pagado el Pedido.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
