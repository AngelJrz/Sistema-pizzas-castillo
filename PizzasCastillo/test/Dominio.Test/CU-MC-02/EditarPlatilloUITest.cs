using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;

namespace Dominio.Test.CU_MC_02
{
    [TestClass]
    public class EditarPlatilloUITest
    {
        public EditarPlatilloUITest()
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

            /*var revisarPlatilloBoton = sesion.FindElementByAccessibilityId("RevisarPlatillo");
            revisarPlatilloBoton.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);
            */

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
        public void EditarPlatilloUI01()
        {

            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Lasagna Familiar");

            var precio = sesion.FindElementByAccessibilityId("PrecioText");
            precio.Clear();
            precio.SendKeys("200");

            var botonFoto = sesion.FindElementByAccessibilityId("FotoBoton");
            botonFoto.Click();
            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var receta = sesion.FindElementByAccessibilityId("recetaText");
            receta.SendKeys("texto solo esta de prueba para probar pruebas echas por probadores de software");


            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            var botonRegistro = sesion.FindElementByAccessibilityId("ActualizarPlatillo");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());

            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Se ha actualizado el platillo y sus ingredientes con exito";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }

        [TestMethod]
        public void EditarPlatilloUI02()
        {
            
            var nombre = sesion.FindElementByAccessibilityId("NombreText");
            nombre.Clear();
            nombre.SendKeys("Lasagna Grande");

            var botonRegistro = sesion.FindElementByAccessibilityId("ActualizarPlatillo");
            botonRegistro.Click();

            sesion.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            sesion.SwitchTo().Window(sesion.WindowHandles.First());
            
            var mensaje = sesion.FindElementByAccessibilityId("Mensaje");
            string mensajeEsperado = "Selecciona una opción";
            Assert.AreEqual(mensaje.Text, mensajeEsperado);
        }
    }
}
