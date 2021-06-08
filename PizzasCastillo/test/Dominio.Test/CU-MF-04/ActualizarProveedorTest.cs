using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dominio.Test.CU_MF_04
{
    [TestClass]
    public class ActualizarProveedorTest
    {
        private ProveedorController proveedorController = new ProveedorController();

        [TestMethod]
        public void ActualizarProveedor()
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
                Id = 4,
                Nombre = "Pollos Crazy",
                NombreArchivo = "Imagen.jpg",
                Email = "Negocio@pollos.com",
                Telefono = "2281887144",
                Direccion = direccion,
                Dni = "fasdf321ra",
                ListaDeProductos = arr1,
                NombreEncargado = "Jorge ArmandoXDD",
            };

            ResultadoRegistro resultado = proveedorController.ActualizarProovedor(proveedorARegistrar, false);

            Assert.AreEqual(resultado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void ActualizarProveedorDNIExistente()
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

            ResultadoRegistro resultado = proveedorController.ActualizarProovedor(proveedorARegistrar, true);

            Assert.AreEqual(resultado, ResultadoRegistro.UsuarioYaExiste);
        }

        [TestMethod]
        public void ActualizarProveedorIncorrecto()
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
                Dni = "asccc",
                ListaDeProductos = arr1,
                NombreEncargado = "Jwww",
            };

            ResultadoRegistro resultado = proveedorController.ActualizarProovedor(proveedorARegistrar,true);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }


        [TestMethod]
        public void ActualizarProveedorSinDireccion()
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

            ResultadoRegistro resultado = proveedorController.ActualizarProovedor(proveedorARegistrar,false);

            Assert.AreEqual(resultado, ResultadoRegistro.DireccionNoEspecificada);
        }

        [TestMethod]
        public void ActualizarProveedorNulo()
        {

            Proveedor proveedorARegistrar = null;

            ResultadoRegistro resultado = proveedorController.ActualizarProovedor(proveedorARegistrar,false);

            Assert.AreEqual(resultado, ResultadoRegistro.InformacionIncorrecta);
        }
    }
}
