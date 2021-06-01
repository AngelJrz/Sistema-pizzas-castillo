using Dominio.Enumeraciones;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Test.CU_MU_05
{
    /// <summary>
    /// Descripción resumida de GestionarTiposUsuariosTest
    /// </summary>
    [TestClass]
    public class GestionarTiposUsuariosTest
    {
        public GestionarTiposUsuariosTest()
        {
            _tipoUsuarioController = new TipoUsuarioController();
        }

        private TestContext testContextInstance;
        private TipoUsuarioController _tipoUsuarioController;

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
        public void GestionarTiposUsuarios01()
        {
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = "Repartidor"
            };

            ResultadoRegistro resultado = _tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void GestionarTiposUsuarios02()
        {
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = ""
            };

            ResultadoRegistro resultado = _tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void GestionarTiposUsuarios03()
        {
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = "Repartidor"
            };

            ResultadoRegistro resultado = _tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);

            Assert.AreEqual(resultado, ResultadoRegistro.TipoUsuarioYaExiste);
        }

        [TestMethod]
        public void GestionarTiposUsuarios04()
        {
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = "Tipo usuario prueba con un nombre largo que superar el limite permitido"
            };

            ResultadoRegistro resultado = _tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void GestionarTiposUsuarios05()
        {
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = "Tipo"
            };

            ResultadoRegistro resultado = _tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void GestionarTiposUsuarios06()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor"));

            tipoUsuarioActualizado.Nombre = "Repartidor principal";

            ResultadoRegistro resultado = _tipoUsuarioController.EditarTipoUsuario(tipoUsuarioActualizado,
                                                                                   seActualizoNombre: true);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void GestionarTiposUsuarios07()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor principal"));

            tipoUsuarioActualizado.Nombre = "";

            ResultadoRegistro resultado = _tipoUsuarioController.EditarTipoUsuario(tipoUsuarioActualizado,
                                                                                   seActualizoNombre: true);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void GestionarTiposUsuarios08()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor principal"));

            tipoUsuarioActualizado.Nombre = "Mesero";

            ResultadoRegistro resultado = _tipoUsuarioController.EditarTipoUsuario(tipoUsuarioActualizado,
                                                                                   seActualizoNombre: true);

            Assert.AreEqual(resultado, ResultadoRegistro.TipoUsuarioYaExiste);
        }

        [TestMethod]
        public void GestionarTiposUsuarios09()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor principal"));

            tipoUsuarioActualizado.Estatus = 0;

            ResultadoRegistro resultado = _tipoUsuarioController.EditarTipoUsuario(tipoUsuarioActualizado);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void GestionarTiposUsuarios10()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor principal"));

            tipoUsuarioActualizado.Estatus = 1;

            ResultadoRegistro resultado = _tipoUsuarioController.EditarTipoUsuario(tipoUsuarioActualizado);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void GestionarTiposUsuarios11()
        {
            List<Tipo> tiposUsuarios = _tipoUsuarioController.ObtenerTiposUsuario();
            Tipo tipoUsuarioActualizado = tiposUsuarios.Find(tipo => tipo.Nombre.Equals("Repartidor principal"));

            ResultadoRegistro resultado = _tipoUsuarioController.DarDeBajaTipoUsuario(tipoUsuarioActualizado);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void GestionarTiposUsuarios12()
        {

            Tipo tipoUsuarioActualizado = null;

            ResultadoRegistro resultado = _tipoUsuarioController.DarDeBajaTipoUsuario(tipoUsuarioActualizado);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }
    }
}
