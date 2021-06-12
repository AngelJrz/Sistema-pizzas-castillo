using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MU_04
{
    /// <summary>
    /// Descripción resumida de IniciarSesionUITest
    /// </summary>
    [TestClass]
    public class IniciarSesionUITest
    {
        public IniciarSesionUITest()
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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            if (sesion != null)
            {
                sesion.Close();
                sesion.Quit();
            }
        }
        //
        #endregion

        [TestMethod]
        public void IniciarSesionUI01()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("rodrigol");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("rodrigol");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var tipoUsuarioEnSesion = sesion.FindElementByAccessibilityId("TipoUsuarioText");
            Assert.AreEqual(tipoUsuarioEnSesion.Text, "Gerente");
        }

        [TestMethod]
        public void IniciarSesionUI02()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("caro123");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("caro123");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var tipoUsuarioEnSesion = sesion.FindElementByAccessibilityId("TipoUsuarioText");
            Assert.AreEqual(tipoUsuarioEnSesion.Text, "Encargado de caja");
        }

        [TestMethod]
        public void IniciarSesionUI03()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("antonioh");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("antonioh");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var tipoUsuarioEnSesion = sesion.FindElementByAccessibilityId("TipoUsuarioText");
            Assert.AreEqual(tipoUsuarioEnSesion.Text, "Contador");
        }

        [TestMethod]
        public void IniciarSesionUI04()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("marianar");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("marianar");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var tipoUsuarioEnSesion = sesion.FindElementByAccessibilityId("TipoUsuarioText");
            Assert.AreEqual(tipoUsuarioEnSesion.Text, "Mesero");
        }

        [TestMethod]
        public void IniciarSesionUI05()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("carlosh");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("carlosh");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var tipoUsuarioEnSesion = sesion.FindElementByAccessibilityId("TipoUsuarioText");
            Assert.AreEqual(tipoUsuarioEnSesion.Text, "Cocinero");
        }

        [TestMethod]
        public void IniciarSesionUI06()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("usuarioNoExiste");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("contrasena");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El usuario y/o contraseña son incorrectos. Por favor verifiquelos e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void IniciarSesionUI07()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("rodrigol");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("rodrigol2");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El usuario y/o contraseña son incorrectos. Por favor verifiquelos e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void IniciarSesionUI08()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            usuarioTextBox.SendKeys("rodrigolUser");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("rodrigol");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El usuario y/o contraseña son incorrectos. Por favor verifiquelos e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void IniciarSesionUI09()
        {
            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Por favor ingrese el usuario y contraseña.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void IniciarSesionUI10()
        {
            var usuarioTextBox = sesion.FindElementByAccessibilityId("usuarioText");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");

            for (int i = 0; i <= 3; i++)
            {
                sesion.SwitchTo().Window(sesion.WindowHandles.First());
                usuarioTextBox.SendKeys("usuarioNoExiste");
                passwordText.SendKeys("contrasena");

                iniciarSesionBoton.Click();

                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                sesion.SwitchTo().Window(sesion.WindowHandles.First());
                var aceptarBoton = sesion.FindElementByAccessibilityId("Aceptar");

                if (i < 3)
                    aceptarBoton.Click();
            }
            

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Ha alcanzado el límite intentos de iniciar sesión. Por favor espere 5 minutos.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
