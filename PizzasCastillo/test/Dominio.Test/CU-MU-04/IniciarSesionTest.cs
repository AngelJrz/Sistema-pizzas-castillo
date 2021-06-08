using Dominio.Entidades;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Test.CU_MU_04
{
    /// <summary>
    /// Descripción resumida de IniciarSesionTest
    /// </summary>
    [TestClass]
    public class IniciarSesionTest
    {
        public IniciarSesionTest()
        {
            _empleadoController = new EmpleadoController();
        }

        private TestContext testContextInstance;
        private readonly EmpleadoController _empleadoController;

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
        public void IniciarSesion01()
        {
            string username = "rodrigol";
            string contrasenia = "rodrigol";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);

            string nombreTipoUsuario = "Gerente";

            Assert.IsTrue(empleadoAIniciarSesion != null
                && empleadoAIniciarSesion.TipoUsuario.Nombre.Equals(nombreTipoUsuario));
        }

        [TestMethod]
        public void IniciarSesion02()
        {
            string username = "caro123";
            string contrasenia = "caro123";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);

            string nombreTipoUsuario = "Encargado de caja";

            Assert.IsTrue(empleadoAIniciarSesion != null
                && empleadoAIniciarSesion.TipoUsuario.Nombre.Equals(nombreTipoUsuario));
        }

        [TestMethod]
        public void IniciarSesion03()
        {
            string username = "antonioh";
            string contrasenia = "antonioh";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);

            string nombreTipoUsuario = "Contador";

            Assert.IsTrue(empleadoAIniciarSesion != null
                && empleadoAIniciarSesion.TipoUsuario.Nombre.Equals(nombreTipoUsuario));
        }

        [TestMethod]
        public void IniciarSesion04()
        {
            string username = "marianar";
            string contrasenia = "marianar";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);

            string nombreTipoUsuario = "Mesero";

            Assert.IsTrue(empleadoAIniciarSesion != null
                && empleadoAIniciarSesion.TipoUsuario.Nombre.Equals(nombreTipoUsuario));
        }

        [TestMethod]
        public void IniciarSesion05()
        {
            string username = "carlosh";
            string contrasenia = "carlosh";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);

            string nombreTipoUsuario = "Cocinero";

            Assert.IsTrue(empleadoAIniciarSesion != null
                && empleadoAIniciarSesion.TipoUsuario.Nombre.Equals(nombreTipoUsuario));
        }

        [TestMethod]
        public void IniciarSesion06()
        {
            string username = "usuarioNoExiste";
            string contrasenia = "contrasena";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);


            Assert.IsNull(empleadoAIniciarSesion);
        }

        [TestMethod]
        public void IniciarSesion07()
        {
            string username = "rodrigol";
            string contrasenia = "rodrigol2";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);


            Assert.IsNull(empleadoAIniciarSesion);
        }

        [TestMethod]
        public void IniciarSesion08()
        {
            string username = "rodrigolUser";
            string contrasenia = "rodrigol";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);


            Assert.IsNull(empleadoAIniciarSesion);
        }

        [TestMethod]
        public void IniciarSesion09()
        {
            string username = "";
            string contrasenia = "";

            Empleado empleadoAIniciarSesion = _empleadoController.IniciarSesion(username, contrasenia);


            Assert.IsNull(empleadoAIniciarSesion);
        }
    }
}
