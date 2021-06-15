using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MU_02
{
    /// <summary>
    /// Descripción resumida de EditarUsuarioUITest
    /// </summary>
    [TestClass]
    public class EditarUsuarioUITest
    {
        public EditarUsuarioUITest()
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
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EditarEmpleadoUI01()
        {
            var botonEditar = sesion.FindElementByAccessibilityId("usuarioPrueba");
            botonEditar.Click();

            var email = sesion.FindElementByAccessibilityId("EmailText");
            email.Clear();
            email.SendKeys("cesar_mp@hotmail.com");

            var telefono = sesion.FindElementByAccessibilityId("TelefonoText");
            telefono.Clear();
            telefono.SendKeys("2281102131");

            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.Clear();
            salario.SendKeys("1850");

            var botonActualizar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonActualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El empleado fue actualizado correctamente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EditarEmpleadoUI02()
        {
            var botonEditar = sesion.FindElementByAccessibilityId("usuarioPrueba");
            botonEditar.Click();

            var username = sesion.FindElementByAccessibilityId("UsernameText");
            username.Clear();
            username.SendKeys("usuarioPrueba1");

            var botonActualizar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonActualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El empleado fue actualizado correctamente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EditarEmpleadoUI03()
        {
            var botonEditar = sesion.FindElementByAccessibilityId("usuarioPrueba1");
            botonEditar.Click();

            var username = sesion.FindElementByAccessibilityId("UsernameText");
            username.Clear();
            username.SendKeys("rodrigol");

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            var botonActualizar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonActualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = $"El usuario {username.Text} ya pertenece a otro empleado. Por favor ingrese uno diferente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EditarEmpleadoUI04()
        {
            var botonEditar = sesion.FindElementByAccessibilityId("usuarioPrueba1");
            botonEditar.Click();

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();

            var apellidos = sesion.FindElementByAccessibilityId("ApellidosText");
            apellidos.Clear();

            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.Clear();
            salario.SendKeys("0");

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            var botonActualizar = sesion.FindElementByAccessibilityId("ActualizarBoton");
            botonActualizar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
