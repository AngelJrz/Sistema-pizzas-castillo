using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MU_05
{
    /// <summary>
    /// Descripción resumida de GestionarTiposUsuariosUITest
    /// </summary>
    [TestClass]
    public class GestionarTiposUsuariosUITest
    {
        public GestionarTiposUsuariosUITest()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;
        protected static WindowsDriver<WindowsElement> sesion;
        protected const string DRIVER_URL = "http://127.0.0.1:4723";
        private const string APP_ID = @"D:\pizzas-castillo\PizzasCastillo\Presentacion\bin\Debug\Presentacion.exe";

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
            usuarioTextBox.SendKeys("rodrigol");
            var passwordText = sesion.FindElementByAccessibilityId("passwordText");
            passwordText.SendKeys("rodrigol");

            var iniciarSesionBoton = sesion.FindElementByAccessibilityId("iniciarSesionBoton");
            iniciarSesionBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var seccionUsuarios = sesion.FindElementByAccessibilityId("Usuarios");
            seccionUsuarios.Click();
        }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GestionarTiposUsuariosUI01()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var agregarBoton = sesion.FindElementByAccessibilityId("AgregarBoton");
            agregarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Encargado de inventario");

            var registrarBoton = sesion.FindElementByAccessibilityId("RegistrarBoton");
            registrarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Nuevo tipo registrado exitosamente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI02()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var agregarBoton = sesion.FindElementByAccessibilityId("AgregarBoton");
            agregarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var registrarBoton = sesion.FindElementByAccessibilityId("RegistrarBoton");
            registrarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Por favor ingresa el nombre del nuevo tipo";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI03()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var agregarBoton = sesion.FindElementByAccessibilityId("AgregarBoton");
            agregarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Gerente");

            var registrarBoton = sesion.FindElementByAccessibilityId("RegistrarBoton");
            registrarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El tipo usuario ya se encuentra registrado. Por favor verifique la información.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI04()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var actualizar = sesion.FindElementByAccessibilityId("Repartidor");
            actualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Repartidor prueba");

            var actualizarBoton = sesion.FindElementByAccessibilityId("ActualizarBoton");
            actualizarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Tipo usuario actualizado exitosamente";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI05()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var actualizar = sesion.FindElementByAccessibilityId("Repartidor prueba");
            actualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Gerente");

            var actualizarBoton = sesion.FindElementByAccessibilityId("ActualizarBoton");
            actualizarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El nombre del tipo usuario ya existe. Por favor verifique la información.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI06()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var eliminar = sesion.FindElementByAccessibilityId("17");
            eliminar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());


            var aceptarBoton = sesion.FindElementByAccessibilityId("AceptarBoton");
            aceptarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El tipo usuario fue dado de baja exitosamente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void GestionarTiposUsuariosUI07()
        {
            var tiposUsuarioBoton = sesion.FindElementByAccessibilityId("TiposUsuarioBoton");
            if (tiposUsuarioBoton.Displayed)
            {
                sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                tiposUsuarioBoton.Click();
            }

            var eliminar = sesion.FindElementByAccessibilityId("1");
            eliminar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());


            var aceptarBoton = sesion.FindElementByAccessibilityId("AceptarBoton");
            aceptarBoton.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            string nombreTipoUsuario = "Gerente";
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = $"No se puede eliminar el tipo de usuario {nombreTipoUsuario} porque ya hay usuarios registrados pertenecientes a él.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
