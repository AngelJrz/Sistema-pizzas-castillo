using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MC_01
{
    [TestClass]
    public class RegistrarPlatilloUITest
    {
        public RegistrarPlatilloUITest()
        {
        }
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

            var seccionUsuarios = sesion.FindElementByAccessibilityId("Platillos");
            seccionUsuarios.Click();

            var registrarUsuarioBoton = sesion.FindElementByAccessibilityId("RegistrarPlatillo");
            registrarUsuarioBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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
        public void RegistrarPlatilloUI01()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Lasagna Grande");
            
            var precio = sesion.FindElementByAccessibilityId("PrecioText");
            precio.SendKeys("80");

            var botonFoto = sesion.FindElementByAccessibilityId("FotoBoton");
            botonFoto.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            var receta = sesion.FindElementByAccessibilityId("recetaText");
            receta.SendKeys("Primero agregamos un nuevo tomate a el platillo y ya por que todo este texto solo esta de prueba para probar pruebas echas por probadores de software");
            
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

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarPlatillo");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha guardado el platillo y sus ingredientes con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarPlatilloUI02()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Lasagna Grande");

            var precio = sesion.FindElementByAccessibilityId("PrecioText");
            precio.SendKeys("1234");

            var botonFoto = sesion.FindElementByAccessibilityId("FotoBoton");
            botonFoto.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var receta = sesion.FindElementByAccessibilityId("recetaText");
            receta.SendKeys("Primero agregamos un nuevo tomate a el platillo y ya por que todo este texto solo esta de prueba para probar pruebas echas por probadores de software");

            var botonAgregarIngrediente = sesion.FindElementByAccessibilityId("AgregarIngrediente");
            botonAgregarIngrediente.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var listaProductos1 = sesion.FindElementByAccessibilityId("ProductosBox");
            listaProductos1.SendKeys("queso");

            var cantidad1 = sesion.FindElementByAccessibilityId("CantidadText");
            cantidad1.SendKeys("2.5");

            var botonAgregarIngrediente1 = sesion.FindElementByAccessibilityId("IngredienteBoton");
            botonAgregarIngrediente1.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarPlatillo");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Este Platilo ya se encuentra registrado, de preferencia editelo mejor";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarPlatilloUI03()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Lasagna Grande");

            var precio = sesion.FindElementByAccessibilityId("PrecioText");
            precio.SendKeys("1234");

            var receta = sesion.FindElementByAccessibilityId("recetaText");
            receta.SendKeys("Primero agregamos un nuevo tomate a el platillo y ya por que todo este texto solo esta de prueba para probar pruebas echas por probadores de software");

            var botonAgregarIngrediente = sesion.FindElementByAccessibilityId("AgregarIngrediente");
            botonAgregarIngrediente.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var listaProductos1 = sesion.FindElementByAccessibilityId("ProductosBox");
            listaProductos1.SendKeys("queso");

            var cantidad1 = sesion.FindElementByAccessibilityId("CantidadText");
            cantidad1.SendKeys("2.5");

            var botonAgregarIngrediente1 = sesion.FindElementByAccessibilityId("IngredienteBoton");
            botonAgregarIngrediente1.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarPlatillo");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "No se ha agregado una foto porfavor seleccione una";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void RegistrarPlatilloUI04()
        {
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.SendKeys("Lasagna Grande");

            var precio = sesion.FindElementByAccessibilityId("PrecioText");
            precio.SendKeys("1234");

            var botonFoto = sesion.FindElementByAccessibilityId("FotoBoton");
            botonFoto.Click();

            var receta = sesion.FindElementByAccessibilityId("recetaText");
            receta.SendKeys("Primero agregamos un nuevo tomate a el platillo y ya por que todo este texto solo esta de prueba para probar pruebas echas por probadores de software");

            var botonRegistro = sesion.FindElementByAccessibilityId("RegistrarPlatillo");
            botonRegistro.Click();
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Uno o mas campos estan vacios y/o incorrectos, verificar porfavor";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
