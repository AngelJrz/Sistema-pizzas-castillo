using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MU_06
{
    [TestClass]
    public class RegistroClienteUITest
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

            var pedidosLabel = sesion.FindElementByAccessibilityId("RegistroCliente");
            pedidosLabel.Click();

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
        public void RegistroClienteUI1()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Armando ");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Gonzales Chorro");

            var EmailTextBox = sesion.FindElementByAccessibilityId("EmailText");
            EmailTextBox.SendKeys("correolargo@hotmail.com");

            var TelefonoTextBox = sesion.FindElementByAccessibilityId("TelefonoText");
            TelefonoTextBox.SendKeys("2281441780");

            var CalleTextBox = sesion.FindElementByAccessibilityId("CalleText");
            CalleTextBox.SendKeys("Calle pavimentada amaranto esperanza");

            var ColoniaTextBox = sesion.FindElementByAccessibilityId("ColoniaText");
            ColoniaTextBox.SendKeys("Las lomas");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.SendKeys("53");

            var CodigoPostalTextBox = sesion.FindElementByAccessibilityId("CodigoPostalText");
            CodigoPostalTextBox.SendKeys("92698");

            var CiudadTextBox = sesion.FindElementByAccessibilityId("CiudadText");
            CiudadTextBox.SendKeys("Tezmacoco");

            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");
            
            var ReferenciasTextBox = sesion.FindElementByAccessibilityId("ReferenciasText");
            ReferenciasTextBox.SendKeys("Afuera hay una ferreteria Los hermanos, la casa es color verde y esta en obra negra aun");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarCliente");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado el cliente y su direccion con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistroClienteUI2()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Armando ");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Gonzales Chorro");
            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarCliente");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Uno o mas campos estan incorrectos,verificar bien";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistroClienteUI3()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Armando ");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Gonzales Chorro");

            var EmailTextBox = sesion.FindElementByAccessibilityId("EmailText");
            EmailTextBox.SendKeys("correolargo@hotmail.com");

            var TelefonoTextBox = sesion.FindElementByAccessibilityId("TelefonoText");
            TelefonoTextBox.SendKeys("2281441780");

            var CalleTextBox = sesion.FindElementByAccessibilityId("CalleText");
            CalleTextBox.SendKeys("Calle pavimentada amaranto esperanza");

            var ColoniaTextBox = sesion.FindElementByAccessibilityId("ColoniaText");
            ColoniaTextBox.SendKeys("Las lomas");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.SendKeys("53");

            var CodigoPostalTextBox = sesion.FindElementByAccessibilityId("CodigoPostalText");
            CodigoPostalTextBox.SendKeys("92698");

            var CiudadTextBox = sesion.FindElementByAccessibilityId("CiudadText");
            CiudadTextBox.SendKeys("Tezmacoco");

            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");

            var ReferenciasTextBox = sesion.FindElementByAccessibilityId("ReferenciasText");
            ReferenciasTextBox.SendKeys("Afuera hay una ferreteria Los hermanos, la casa es color verde y esta en obra negra aun");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarCliente");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Este cliente ya se encuentra registrado";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
