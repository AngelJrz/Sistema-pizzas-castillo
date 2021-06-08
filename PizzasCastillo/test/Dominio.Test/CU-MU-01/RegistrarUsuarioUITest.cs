using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Test.CU_MU_01
{
    /// <summary>
    /// Descripción resumida de RegistrarUsuarioUITest
    /// </summary>
    [TestClass]
    public class RegistrarUsuarioUITest
    {
        public RegistrarUsuarioUITest()
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

            var registrarUsuarioBoton = sesion.FindElementByAccessibilityId("RegistrarUsuarioBoton");
            registrarUsuarioBoton.Click();
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
        public void RegistrarEmpleadoUI01()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("David");

            var apellidos = sesion.FindElementByAccessibilityId("ApellidosText");
            apellidos.SendKeys("Armengou Payes");

            var email = sesion.FindElementByAccessibilityId("EmailText");
            email.SendKeys("davidap@gmail.com");

            var telefono = sesion.FindElementByAccessibilityId("TelefonoText");
            telefono.SendKeys("2281152322");

            var calle = sesion.FindElementByAccessibilityId("CalleText");
            calle.SendKeys("Av Avila Camacho");

            var noExterior = sesion.FindElementByAccessibilityId("NoExteriorText");
            noExterior.SendKeys("21");

            var codigoPostal = sesion.FindElementByAccessibilityId("CodigoPostalText");
            codigoPostal.SendKeys("91025");

            var ciudad = sesion.FindElementByAccessibilityId("CiudadText");
            ciudad.SendKeys("Xalapa");

            var colonia = sesion.FindElementByAccessibilityId("ColoniaText");
            colonia.SendKeys("Centro");

            var listaEntidadesFederativas = sesion.FindElementByAccessibilityId("ListaEntidadesFederativas");
            listaEntidadesFederativas.SendKeys("Veracruz");

            var referencias = sesion.FindElementByAccessibilityId("ReferenciasText");
            referencias.SendKeys("Enfrente del Oxxo rojo");

            var tipoUsuario = sesion.FindElementByAccessibilityId("ListaTiposUsuario");
            tipoUsuario.SendKeys("Mesero");

            var username = sesion.FindElementByAccessibilityId("UsernameText");
            username.SendKeys("davidap");

            var contrasenia = sesion.FindElementByAccessibilityId("PasswordText");
            contrasenia.SendKeys("davidap");

            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.SendKeys("1500.50");

            var botonRegistrar = sesion.FindElementByAccessibilityId("RegistrarBoton");
            botonRegistrar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "El empleado fue registrado correctamente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarEmpleadoUI02()
        {

            var email = sesion.FindElementByAccessibilityId("EmailText");
            email.SendKeys("davidap@gmail.com");

            var telefono = sesion.FindElementByAccessibilityId("TelefonoText");
            telefono.SendKeys("2281152322");

            var calle = sesion.FindElementByAccessibilityId("CalleText");
            calle.SendKeys("Av Avila Camacho");

            var noExterior = sesion.FindElementByAccessibilityId("NoExteriorText");
            noExterior.SendKeys("21");

            var codigoPostal = sesion.FindElementByAccessibilityId("CodigoPostalText");
            codigoPostal.SendKeys("91025");

            var ciudad = sesion.FindElementByAccessibilityId("CiudadText");
            ciudad.SendKeys("Xalapa");

            var colonia = sesion.FindElementByAccessibilityId("ColoniaText");
            colonia.SendKeys("Centro");

            var listaEntidadesFederativas = sesion.FindElementByAccessibilityId("ListaEntidadesFederativas");
            listaEntidadesFederativas.SendKeys("Veracruz");

            var referencias = sesion.FindElementByAccessibilityId("ReferenciasText");
            referencias.SendKeys("Enfrente del Oxxo rojo");

            var tipoUsuario = sesion.FindElementByAccessibilityId("ListaTiposUsuario");
            tipoUsuario.SendKeys("Mesero");

            var username = sesion.FindElementByAccessibilityId("UsernameText");
            username.SendKeys("davidap");

            var contrasenia = sesion.FindElementByAccessibilityId("PasswordText");
            contrasenia.SendKeys("davidap");

            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.SendKeys("1500.50");

            var botonRegistrar = sesion.FindElementByAccessibilityId("RegistrarBoton");
            botonRegistrar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarEmpleadoUI03()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("David");

            var apellidos = sesion.FindElementByAccessibilityId("ApellidosText");
            apellidos.SendKeys("Armengou Payes");

            var email = sesion.FindElementByAccessibilityId("EmailText");
            email.SendKeys("davidap@gmail.com");

            var telefono = sesion.FindElementByAccessibilityId("TelefonoText");
            telefono.SendKeys("2281152322");

            var calle = sesion.FindElementByAccessibilityId("CalleText");
            calle.SendKeys("Av Avila Camacho");

            var noExterior = sesion.FindElementByAccessibilityId("NoExteriorText");
            noExterior.SendKeys("21");

            var codigoPostal = sesion.FindElementByAccessibilityId("CodigoPostalText");
            codigoPostal.SendKeys("91025");

            var ciudad = sesion.FindElementByAccessibilityId("CiudadText");
            ciudad.SendKeys("Xalapa");

            var colonia = sesion.FindElementByAccessibilityId("ColoniaText");
            colonia.SendKeys("Centro");

            var listaEntidadesFederativas = sesion.FindElementByAccessibilityId("ListaEntidadesFederativas");
            listaEntidadesFederativas.SendKeys("Veracruz");

            var referencias = sesion.FindElementByAccessibilityId("ReferenciasText");
            referencias.SendKeys("Enfrente del Oxxo rojo");

            var tipoUsuario = sesion.FindElementByAccessibilityId("ListaTiposUsuario");
            tipoUsuario.SendKeys("Mesero");

            var username = sesion.FindElementByAccessibilityId("UsernameText");
            username.SendKeys("davidap");

            var contrasenia = sesion.FindElementByAccessibilityId("PasswordText");
            contrasenia.SendKeys("davidap");

            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.SendKeys("1500.50");

            var botonRegistrar = sesion.FindElementByAccessibilityId("RegistrarBoton");
            botonRegistrar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = $"El usuario {username.Text} ya pertenece a otro empleado. Por favor ingrese uno diferente.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarEmpleadoUI04()
        {
            var salario = sesion.FindElementByAccessibilityId("SalarioText");
            salario.SendKeys("150");
            salario.Clear();


            var botonRegistrar = sesion.FindElementByAccessibilityId("RegistrarBoton");
            botonRegistrar.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
