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
    public class UnitTest1
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
                Telefono = "2288442970",
                Direcciones = new List<Direccion>() { direccion },
                TipoUsuario = tipoUsuario,
            };

            bool resultado = clienteController.GuardarCliente(clienteARegistrar);

            Assert.IsTrue(resultado);
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
                Id = 5,
                Nombres = "Sammy",
                Apellidos = "Guayabo Platano",
                Email = "boing@jugos.com",
                Telefono = "2288442970",
                Direcciones = new List<Direccion>() { direccion },
                TipoUsuario = tipoUsuario,
            };

            bool resultado = clienteController.GuardarCliente(clienteARegistrar);

            Assert.IsFalse(resultado);
        }
    }
}
