using Dominio.Entidades;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Test.CU_MU_03
{
    /// <summary>
    /// Descripción resumida de EliminarUsuarioTest
    /// </summary>
    [TestClass]
    public class EliminarUsuarioTest
    {
        public EliminarUsuarioTest()
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
        public void EliminarUsuario01()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEliminar = _empleadoController.ObtenerEmpleado(username);

            bool resultado = _empleadoController.DarDeBajaEmpleado(empleadoAEliminar.NumeroEmpleado);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EliminarUsuario02()
        {
            string numeroEmpleado = "EMP-002";

            bool resultado = _empleadoController.DarDeBajaEmpleado(numeroEmpleado);

            Assert.IsFalse(resultado);
        }
    }
}
