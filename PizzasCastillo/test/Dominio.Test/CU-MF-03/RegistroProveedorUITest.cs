using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MF_03
{
    [TestClass]
    public class RegistroProveedorUITest
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

            var proveedorLabel = sesion.FindElementByAccessibilityId("Proveedores");
            proveedorLabel.Click();

            var registroProveedorboton = sesion.FindElementByAccessibilityId("RegistrarProveedor");
            registroProveedorboton.Click();
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
        public void RegistroProveedorUI1()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Pollos Hermanos SA de CV");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Giancarlo esposito");

            var EmailTextBox = sesion.FindElementByAccessibilityId("EmailText");
            EmailTextBox.SendKeys("correoPollo@hotmail.com");

            var TelefonoTextBox = sesion.FindElementByAccessibilityId("TelefonoText");
            TelefonoTextBox.SendKeys("221421780");

            var DniTextBox = sesion.FindElementByAccessibilityId("DniText");
            DniTextBox.SendKeys("h98ush9ia1");

            var CalleTextBox = sesion.FindElementByAccessibilityId("CalleText");
            CalleTextBox.SendKeys("Calle de negocios polanco ");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.SendKeys("53");

            var botonArchivo = sesion.FindElementByAccessibilityId("AbrirArchivo");
            botonArchivo.Click();

            var CodigoPostalTextBox = sesion.FindElementByAccessibilityId("CodigoPostalText");
            CodigoPostalTextBox.SendKeys("92698");

            var CiudadTextBox = sesion.FindElementByAccessibilityId("CiudadText");
            CiudadTextBox.SendKeys("Tezmacoco");

            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado el proveedor y su direccion con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistroProveedorUI2()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Pollos Hermanos SA de CV");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Giancarlo esposito");

            var EmailTextBox = sesion.FindElementByAccessibilityId("EmailText");
            EmailTextBox.SendKeys("correoPollo@hotmail.com");

            var TelefonoTextBox = sesion.FindElementByAccessibilityId("TelefonoText");
            TelefonoTextBox.SendKeys("221421780");

            var DniTextBox = sesion.FindElementByAccessibilityId("DniText");
            DniTextBox.SendKeys("h98ush9ia1");

            var CalleTextBox = sesion.FindElementByAccessibilityId("CalleText");
            CalleTextBox.SendKeys("Calle de negocios polanco ");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.SendKeys("53");

            var botonArchivo = sesion.FindElementByAccessibilityId("AbrirArchivo");
            botonArchivo.Click();

            var CodigoPostalTextBox = sesion.FindElementByAccessibilityId("CodigoPostalText");
            CodigoPostalTextBox.SendKeys("92698");

            var CiudadTextBox = sesion.FindElementByAccessibilityId("CiudadText");
            CiudadTextBox.SendKeys("Tezmacoco");

            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Este proveedor ya se encuentra registrado";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistroProveedorUI3()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.SendKeys("Pollos Hermanos SA de CV");

            var ApellidosTextBox = sesion.FindElementByAccessibilityId("ApellidosText");
            ApellidosTextBox.SendKeys("Giancarlo esposito");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.SendKeys("53");

            var CodigoPostalTextBox = sesion.FindElementByAccessibilityId("CodigoPostalText");
            CodigoPostalTextBox.SendKeys("92698");

            var CiudadTextBox = sesion.FindElementByAccessibilityId("CiudadText");
            CiudadTextBox.SendKeys("Tezmacoco");

            var ListaEstadosBox = sesion.FindElementByAccessibilityId("ListaEstados");
            ListaEstadosBox.SendKeys("Veracruz");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Aun no ha agregado un archivo";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
