using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MF_04
{
    [TestClass]
    public class ActualizarProveedorUITest
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

            var registroProveedorboton = sesion.FindElementByAccessibilityId("ListadeProveedores");
            registroProveedorboton.Click();

            var EditarProveedorboton = sesion.FindElementByAccessibilityId("EditarProveedor");
            EditarProveedorboton.Click();
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
        public void ActualizarProveedorUI1()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.Clear();
            nombresTextBox.SendKeys("Pollos Hermanosos Publicos");

            var DniTextBox = sesion.FindElementByAccessibilityId("DniText");
            DniTextBox.Clear();
            DniTextBox.SendKeys("ad69sgaer1");


            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.Clear(); 
            NoExteriorTextBox.SendKeys("67");


            var botonRegistro = sesion.FindElementByAccessibilityId("ActualizarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha actualizado el proveedor y su direccion con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void ActualizarProveedorUI2()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.Clear();
            nombresTextBox.SendKeys("Pollos Hermanosos Publicos");

            var DniTextBox = sesion.FindElementByAccessibilityId("DniText");
            DniTextBox.Clear();
            DniTextBox.SendKeys("h98ush9ia1");

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.Clear();
            NoExteriorTextBox.SendKeys("67");


            var botonRegistro = sesion.FindElementByAccessibilityId("ActualizarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Ya se encuentra un proveedor registrado con el mismo DNI, este debe ser unico por empresa";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void ActualizarProveedorUI3()
        {
            var nombresTextBox = sesion.FindElementByAccessibilityId("NombresText");
            nombresTextBox.Clear();

            var DniTextBox = sesion.FindElementByAccessibilityId("DniText");
            DniTextBox.Clear();

            var NoExteriorTextBox = sesion.FindElementByAccessibilityId("NoExteriorText");
            NoExteriorTextBox.Clear();


            var botonRegistro = sesion.FindElementByAccessibilityId("ActualizarProveedor");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Uno o mas campos estan incorrectos y/o vacios,verificar bien";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
