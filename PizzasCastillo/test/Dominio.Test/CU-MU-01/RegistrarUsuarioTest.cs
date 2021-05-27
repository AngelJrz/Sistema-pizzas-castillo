using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;

namespace Dominio.Test.CU_MU_01
{
    /// <summary>
    /// Descripción resumida de RegistrarUsuarioTest
    /// </summary>
    [TestClass]
    public class RegistrarUsuarioTest
    {
        private EmpleadoController _empleadoController;

        public RegistrarUsuarioTest()
        {
            _empleadoController = new EmpleadoController();
        }

        private TestContext testContextInstance;

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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void RegistrarEmpleado01()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Direccion direccion = new Direccion
            {
                Calle = "Av Avila Camacho",
                NumeroExterior = "21",
                CodigoPostal = "91025",
                Colonia = "Centro",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa",
                Referencias = "Enfrente del Oxxo rojo",
                NumeroInterior = ""
            };

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Mesero"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Cesar Segio",
                Apellidos = "Martinez Palacios",
                Email = "cesar@gmail.com",
                Telefono = "2281152322",
                Direcciones = new List<Direccion>() { direccion },
                Username = "usuarioPrueba",
                Contrasenia = "usuarioPrueba",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            bool resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.IsTrue(resultado);
        }
    }
}
