using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;

namespace Dominio.Test.CU_MU_06
{
    [TestClass]
    public class RegistroClienteTest
    {
        private ClienteController clienteController = new ClienteController();

        [TestMethod]
        public void RegistrarCliente()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Direccion direccion = new Direccion
            {
                Calle = "Privada los miraobos",
                NumeroExterior = "88",
                CodigoPostal = "98414",
                Colonia = "Los Miradores",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa",
                Referencias = "Junto a la casa de carlos slim",
                NumeroInterior = ""
            };

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cliente"));

            Persona clienteARegistrar = new Persona
            {
                Nombres = "Armando",
                Apellidos = "Hoyos",
                Email = "hoyo@derbez.com",
                Telefono = "2281887144",
                Direcciones = new List<Direccion>() { direccion },
                TipoUsuario = tipoUsuario,
            };

            ResultadoRegistro resultado = clienteController.RegistrarCliente(clienteARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void RegistrarClienteExistente()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Direccion direccion = new Direccion
            {
                Calle = "Privada los miraobos",
                NumeroExterior = "87",
                CodigoPostal = "98414",
                Colonia = "Los Miradores",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa",
                Referencias = "Junto a la casa de carlos slim",
                NumeroInterior = ""
            };

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cliente"));

            Persona clienteARegistrar = new Persona
            {
                Nombres = "Sammy",
                Apellidos = "Guayabo Platano",
                Email = "boing@jugos.com",
                Telefono = "2281887144",
                Direcciones = new List<Direccion>() { direccion },
                TipoUsuario = tipoUsuario,
            };

            ResultadoRegistro resultado = clienteController.RegistrarCliente(clienteARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.UsuarioYaExiste);
        }

        [TestMethod]
        public void RegistrarClienteIncorrecto()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Direccion direccion = new Direccion
            {
                Calle = "Privada los miraobos",
                NumeroExterior = "",
                CodigoPostal = "98414",
                Colonia = "Los Mres",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa",
                Referencias = "im",
                NumeroInterior = ""
            };

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cliente"));

            Persona clienteARegistrar = new Persona
            {
                Nombres = "Sammy",
                Apellidos = "",
                Email = "boios.com",
                Telefono = "22970",
                Direcciones = new List<Direccion>() { direccion },
                TipoUsuario = tipoUsuario,
            };

            ResultadoRegistro resultado = clienteController.RegistrarCliente(clienteARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }


        [TestMethod]
        public void RegistrarClienteSinDireccion()
        {
            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            List<Tipo> tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            Tipo tipoUsuario = tiposUsuario.Find(tipo => tipo.Nombre.Equals("Cliente"));

            Persona clienteARegistrar = new Persona
            {
                Nombres = "Sammy",
                Apellidos = "Wayabo",
                Email = "boing@jugos.com",
                Telefono = "22970",
                TipoUsuario = tipoUsuario,
            };

            ResultadoRegistro resultado = clienteController.RegistrarCliente(clienteARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.DireccionNoEspecificada);
        }

        [TestMethod]
        public void RegistrarClienteNulo()
        {

            Persona persona = null;

            ResultadoRegistro resultado = clienteController.RegistrarCliente(persona);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }

    }
}
