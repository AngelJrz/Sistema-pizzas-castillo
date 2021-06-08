using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Dominio.Test.CU_MF_03
{
    [TestClass]
    public class RegistroProveedorTest
    {
        private ProveedorController proveedorController = new ProveedorController();

        [TestMethod]
        public void RegistrarProveedor()
        {
            DireccionProveedor direccion = new DireccionProveedor
            {
                Calle = "De negocios grandes",
                Numero = "190",
                CodigoPostal = "23424",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa"
            };

            byte[] arr1 = { 0, 100, 120, 210, 255 };

            Proveedor proveedorARegistrar = new Proveedor
            {
                Nombre = "Pollos Locos",
                NombreArchivo = "Imagen.jpg",
                Email = "Negocio@pollos.com",
                Telefono = "2281887144",
                Direccion = direccion,
                Dni = "fasdf321ra",
                ListaDeProductos = arr1,
                NombreEncargado="Jorge Nitales",
            };

            ResultadoRegistro resultado = proveedorController.GuardarProveedor(proveedorARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void RegistrarProveedorExistente()
        {
            DireccionProveedor direccion = new DireccionProveedor
            {
                Calle = "De negocios grandes",
                Numero = "190",
                CodigoPostal = "23424",
                EntidadFederativa = "Veracruz",
                Ciudad = "Xalapa"
            };

            byte[] arr1 = { 0, 100, 120, 210, 255 };

            Proveedor proveedorARegistrar = new Proveedor
            {
                Nombre = "Pollos Locos",
                NombreArchivo = "Imagen.jpg",
                Email = "Negocio@pollos.com",
                Telefono = "2281887144",
                Direccion = direccion,
                Dni = "fasdf321ra",
                ListaDeProductos = arr1,
                NombreEncargado = "Jorge Nitales",
            };

            ResultadoRegistro resultado = proveedorController.GuardarProveedor(proveedorARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.UsuarioYaExiste);
        }

        [TestMethod]
        public void RegistrarProveedorIncorrecto()
        {
            DireccionProveedor direccion = new DireccionProveedor
            {
                Calle = "",
                Numero = "190",
                CodigoPostal = "23424",
                EntidadFederativa = "Veracruz",
                Ciudad = "n"
            };

            byte[] arr1 = { 0, 100, 120, 210, 255 };

            Proveedor proveedorARegistrar = new Proveedor
            {
                Nombre = "Pollos Locos",
                NombreArchivo = "Imagen.jpg",
                Email = "",
                Telefono = "24",
                Direccion = direccion,
                Dni = "fasda21",
                ListaDeProductos = arr1,
                NombreEncargado = "Jorge Nitales",
            };

            ResultadoRegistro resultado = proveedorController.GuardarProveedor(proveedorARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }


        [TestMethod]
        public void RegistrarProveedorSinDireccion()
        {

            byte[] arr1 = { 0, 100, 120, 210, 255 };

            Proveedor proveedorARegistrar = new Proveedor
            {
                Nombre = "Pollos pelon",
                NombreArchivo = "Imagen231.jpg",
                Email = "Calvo@pollo.com",
                Telefono = "2280811249",
                Dni = "h8567a",
                ListaDeProductos = arr1,
                NombreEncargado = "Jorge Nitales",
            };

            ResultadoRegistro resultado = proveedorController.GuardarProveedor(proveedorARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.DireccionNoEspecificada);
        }

        [TestMethod]
        public void RegistrarProveedorNulo()
        {

            Proveedor proveedorARegistrar = null;

            ResultadoRegistro resultado = proveedorController.GuardarProveedor(proveedorARegistrar);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }
    }
}
