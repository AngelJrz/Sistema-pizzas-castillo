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
        private readonly EmpleadoController _empleadoController;

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
                Nombres = "Cesar Sergio",
                Apellidos = "Martinez Palacios",
                Email = "cesar@gmail.com",
                Telefono = "2281152322",
                Direcciones = new List<Direccion>() { direccion },
                Username = "usuarioPrueba",
                Contrasenia = "usuarioPrueba",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void RegistrarEmpleado02()
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
                Nombres = "",
                Apellidos = "",
                Email = "cesar@gmail.com",
                Telefono = "2281152322",
                Direcciones = new List<Direccion>() { direccion },
                Username = "usuarioPrueba",
                Contrasenia = "usuarioPrueba",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarEmpleado03()
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
                Nombres = "Alejandro",
                Apellidos = "Lopez",
                Email = "alelop@gmail.com",
                Telefono = "2281152322",
                Direcciones = new List<Direccion>() { direccion },
                Username = "usuarioPrueba",
                Contrasenia = "usuarioPrueba",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.UsuarioYaExiste);
        }

        [TestMethod]
        public void RegistrarEmpleado04()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cocinero"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Alejandro",
                Apellidos = "Lopez",
                Email = "alelop@gmail.com",
                Telefono = "2281152322",
                Username = "alelopez",
                Contrasenia = "alelopez",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.DireccionNoEspecificada);
        }

        [TestMethod]
        public void RegistrarEmpleado05()
        {

            Empleado empleadoARegistrar = null;

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarEmpleado06()
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

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Encargado de caja"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Miguel",
                Apellidos = "Cruz Cuervo",
                Email = "estonoesuncorreo.com",
                Telefono = "2281152322",
                Direcciones = new List<Direccion>() { direccion },
                Username = "miguelcc",
                Contrasenia = "miguelcc",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarEmpleado07()
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

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Encargado de caja"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Miguel",
                Apellidos = "Cruz Cuervo",
                Email = "miguelcc@gmail.com",
                Telefono = "numerotel",
                Direcciones = new List<Direccion>() { direccion },
                Username = "miguelcc",
                Contrasenia = "miguelcc",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(1500.50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarEmpleado08()
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

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Encargado de caja"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Miguel",
                Apellidos = "Cruz Cuervo",
                Email = "miguelcc@gmail.com",
                Telefono = "2551223655",
                Direcciones = new List<Direccion>() { direccion },
                Username = "miguelcc",
                Contrasenia = "miguelcc",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(-50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarEmpleado09()
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

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Encargado de caja"));

            Empleado empleadoARegistrar = new Empleado
            {
                Nombres = "Miguel",
                Apellidos = "Cruz Cuervo",
                Email = "miguelcc@gmail.com",
                Telefono = "2551223655000",
                Direcciones = new List<Direccion>() { direccion },
                Username = "miguelcc",
                Contrasenia = "miguelcc",
                TipoUsuario = tipoUsuario,
                SalarioQuincenal = new decimal(-50)
            };

            ResultadoRegistro resultado = _empleadoController.RegistrarEmpleado(empleadoARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

        
    }
}
