using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Test.CU_MU_02
{
    /// <summary>
    /// Descripción resumida de EditarUsuarioTest
    /// </summary>
    [TestClass]
    public class EditarUsuarioTest
    {
        private readonly EmpleadoController _empleadoController;
        public EditarUsuarioTest()
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
        public void EditarEmpleado01()
        {
            string username = "usuarioPrueba";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            empleadoAEditar.Email = "cesar_mp@gmail.com";
            empleadoAEditar.Telefono = "2281002233";
            empleadoAEditar.SalarioQuincenal = new decimal(1800);

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void EditarEmpleado02()
        {
            string username = "usuarioPrueba";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            string usernameActualizado = "usuarioPrueba1";
            empleadoAEditar.Username = usernameActualizado;

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar,
                                                                                        seActualizoUsername: true);

            Empleado empleadoEditado = _empleadoController.ObtenerEmpleado(usernameActualizado);

            Assert.IsTrue(resultado == ResultadoRegistro.RegistroExitoso
                && empleadoEditado.Username.Equals(usernameActualizado));
        }

        [TestMethod]
        public void EditarEmpleado03()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            string usernameActualizado = "rodrigol";
            empleadoAEditar.Username = usernameActualizado;

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar,
                                                                                        seActualizoUsername: true);

            Assert.AreEqual(resultado, ResultadoRegistro.UsuarioYaExiste);
        }

        [TestMethod]
        public void EditarEmpleado04()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            string contraseniaActualizada = "usuarioPrueba1";
            empleadoAEditar.Contrasenia = contraseniaActualizada;

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar,
                                                                                        seActualizoUsername: false,
                                                                                        seActualizoPassword: true);

            Empleado empleadoActualizado = _empleadoController.IniciarSesion(username, contraseniaActualizada);

            Assert.IsTrue(resultado == ResultadoRegistro.RegistroExitoso
                && empleadoActualizado != null);
        }

        [TestMethod]
        public void EditarEmpleado05()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            empleadoAEditar.Nombres = "";
            empleadoAEditar.Apellidos = "";
            empleadoAEditar.Direcciones[0].Calle = "";
            empleadoAEditar.SalarioQuincenal = 0;

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void EditarEmpleado06()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            empleadoAEditar.Email = "correoIncorrecto.com";
            empleadoAEditar.Telefono = "2221223";

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void EditarEmpleado07()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();
            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cocinero"));

            empleadoAEditar.TipoUsuario = tipoUsuario;

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar);

            Empleado empleadoAEditado = _empleadoController.ObtenerEmpleado(username);

            Assert.IsTrue(resultado == ResultadoRegistro.RegistroExitoso
                && empleadoAEditado.TipoUsuario.Nombre.Equals(tipoUsuario.Nombre));
        }

        [TestMethod]
        public void EditarEmpleado08()
        {
            string username = "usuarioPrueba1";
            Empleado empleadoAEditar = _empleadoController.ObtenerEmpleado(username);

            empleadoAEditar.Telefono = "minumero";

            ResultadoRegistro resultado = _empleadoController.ActualizarEmpleado(empleadoAEditar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }
    }
}
