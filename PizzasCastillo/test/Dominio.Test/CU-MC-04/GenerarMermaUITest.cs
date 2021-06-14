using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MC_04
{
    [TestClass]
    public class GenerarMermaUITest
    {

        private TestContext testContextInstance;
        protected static WindowsDriver<WindowsElement> sesion;
        protected const string DRIVER_URL = "http://127.0.0.1:4723";
        private const string APP_ID = @"D:\ProyectosVisualStudio\Sistema-pizzas-castillo\PizzasCastillo\Presentacion\bin\Debug\Presentacion.exe";

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

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("superchef");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("cocinero");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var pedidosLabel = sesion.FindElementByAccessibilityId("Merma");
            pedidosLabel.Click(); 

             var checarPedido = sesion.FindElementByAccessibilityId("MermaInsumo");
            checarPedido.Click();


        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            //if (sesion != null)
            //{
            //    sesion.Close();
            //    sesion.Quit();
            //}
        }

        #endregion


        [TestMethod]
        public void GenerarMermaUI1()
        {
            var pedidosLabel = sesion.FindElementByAccessibilityId("GenerarMerma");
            pedidosLabel.Click();

            var justificacionText = sesion.FindElementByAccessibilityId("justificacionText");
            justificacionText.SendKeys("Iba con el pedido en las manos y se me resbalo por accidente");

            var botonMerma = sesion.FindElementByAccessibilityId("MermarPedido");
            botonMerma.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado la merma con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GenerarMermaUI2()
        {
            var pedidosLabel = sesion.FindElementByAccessibilityId("GenerarMerma");
            pedidosLabel.Click();

            var justificacionText = sesion.FindElementByAccessibilityId("justificacionText");
            justificacionText.SendKeys("Iba con el pedido en las manos y se me resbalo por accidente otra vez, es que el conserje deja mojado");

            var botonMerma = sesion.FindElementByAccessibilityId("MermarArticulo");
            botonMerma.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado la merma con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GenerarMermaUI3()
        {
            var botonAgregarIngrediente = sesion.FindElementByAccessibilityId("AgregarIngrediente");
            botonAgregarIngrediente.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var listaProductos1 = sesion.FindElementByAccessibilityId("ProductosBox");
            listaProductos1.SendKeys("tomate");

            var cantidad1 = sesion.FindElementByAccessibilityId("CantidadText");
            cantidad1.SendKeys("2.5");

            var botonAgregarIngrediente1 = sesion.FindElementByAccessibilityId("IngredienteBoton");
            botonAgregarIngrediente1.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var justificacionText = sesion.FindElementByAccessibilityId("justificacionText");
            justificacionText.SendKeys("Iba con el pedido en las manos y se me resbalo por accidente otra vez, es que el conserje deja mojado");

            var botonMerma = sesion.FindElementByAccessibilityId("MermarInsumos");
            botonMerma.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado la merma con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
