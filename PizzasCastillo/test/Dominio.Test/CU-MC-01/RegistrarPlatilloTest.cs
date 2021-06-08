﻿using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Dominio.Test.CU_MC_01
{
    [TestClass]
    public class RegistrarPlatilloTest
    {
        private List<ArticuloVenta> productosList;
        [TestMethod]
        public void RegistrarPlatillo()
        {
            List<Consume> consumePlatillo = new List<Consume>();
            Consume consumeNew;
            Producto producto;
            byte[] arr1 = { 0, 100, 120, 210, 255 };
            ArticuloVentaController articuloController = new ArticuloVentaController();
            productosList = articuloController.ObtenerProductos();

            ArticuloVenta nuevoPlatillo = new ArticuloVenta
            {
                Nombre = "Pizza de Chorizo",
                Precio = Convert.ToDecimal("500"),
                Foto = productosList[0].Foto,
                Estatus = 1,
                EsPlatillo = true,
                NombreFoto = productosList[0].NombreFoto,
                Platillo = new Platillo
                {
                    FechaRegisto = DateTime.Now,
                    Receta = "Primero tienes que meterle 100 chapulines a la masa, luego escupirle y dejarla hornear por 5 min, despues ya le pones el huitlacoche",
                }
            };
            
            consumeNew = new Consume
            {
                Platillo = nuevoPlatillo.Platillo,
                Producto = productosList[0].Producto,
                Cantidad = 10,
            };


            consumePlatillo.Add(consumeNew);
            nuevoPlatillo.Platillo.Consume = consumePlatillo;

            ArticuloVentaController articuloVentaController = new ArticuloVentaController();

            ResultadoRegistro guardado = articuloVentaController.GuardarPlatilloVenta(nuevoPlatillo);

            Assert.AreEqual(guardado, ResultadoRegistro.RegistroExitoso);
        }

        [TestMethod]
        public void RegistrarPlatilloExistente()
        {
            List<Consume> consumePlatillo = new List<Consume>();
            Consume consumeNew;
            Producto producto;
            byte[] arr1 = { 0, 100, 120, 210, 255 };
            ArticuloVentaController articuloController = new ArticuloVentaController();
            productosList = articuloController.ObtenerProductos();

            ArticuloVenta nuevoPlatillo = new ArticuloVenta
            {
                Nombre = "Pizza de huitlacoche",
                Precio = Convert.ToDecimal("500"),
                Foto = productosList[0].Foto,
                Estatus = 1,
                EsPlatillo = true,
                NombreFoto = productosList[0].NombreFoto,
                Platillo = new Platillo
                {
                    FechaRegisto = DateTime.Now,
                    Receta = "Primero tienes que meterle 100 chapulines a la masa, luego escupirle y dejarla hornear por 5 min, despues ya le pones el huitlacoche",
                }
            };

            consumeNew = new Consume
            {
                Platillo = nuevoPlatillo.Platillo,
                Producto = productosList[0].Producto,
                Cantidad = 10,
            };


            consumePlatillo.Add(consumeNew);
            nuevoPlatillo.Platillo.Consume = consumePlatillo;

            ArticuloVentaController articuloVentaController = new ArticuloVentaController();

            ResultadoRegistro guardado = articuloVentaController.GuardarPlatilloVenta(nuevoPlatillo);

            Assert.AreEqual(guardado, ResultadoRegistro.YaExiste);
        }

        [TestMethod]
        public void RegistrarPlatilloErroneo()
        {
            List<Consume> consumePlatillo = new List<Consume>();
            Consume consumeNew;
            Producto producto;
            byte[] arr1 = { 0, 100, 120, 210, 255 };
            ArticuloVentaController articuloController = new ArticuloVentaController();
            productosList = articuloController.ObtenerProductos();

            ArticuloVenta nuevoPlatillo = new ArticuloVenta
            {
                Nombre = "",
                Precio = Convert.ToDecimal("500"),
                Foto = productosList[0].Foto,
                Estatus = 1,
                EsPlatillo = true,
                NombreFoto = productosList[0].NombreFoto,
                Platillo = new Platillo
                {
                    Receta = "e",
                }
            };

            consumeNew = new Consume
            {
                Platillo = nuevoPlatillo.Platillo,
                Producto = productosList[0].Producto,
                Cantidad = 10,
            };


            consumePlatillo.Add(consumeNew);
            nuevoPlatillo.Platillo.Consume = consumePlatillo;

            ArticuloVentaController articuloVentaController = new ArticuloVentaController();

            ResultadoRegistro guardado = articuloVentaController.GuardarPlatilloVenta(nuevoPlatillo);

            Assert.AreEqual(guardado, ResultadoRegistro.InformacionIncorrecta);
        }

        [TestMethod]
        public void RegistrarPlatilloNullo()
        {

            ArticuloVenta nuevoPlatillo = new ArticuloVenta();

            ArticuloVentaController articuloVentaController = new ArticuloVentaController();

            Assert.ThrowsException<NullReferenceException>(()=> articuloVentaController.GuardarPlatilloVenta(nuevoPlatillo));
        }

        [TestMethod]
        public void RegistrarPlatilloSinIngredientes()
        {
            List<Consume> consumePlatillo = new List<Consume>();
            Consume consumeNew;
            Producto producto;
            byte[] arr1 = { 0, 100, 120, 210, 255 };
            ArticuloVentaController articuloController = new ArticuloVentaController();
            productosList = articuloController.ObtenerProductos();

            ArticuloVenta nuevoPlatillo = new ArticuloVenta
            {
                Nombre = "",
                Precio = Convert.ToDecimal("500"),
                Foto = productosList[0].Foto,
                Estatus = 1,
                EsPlatillo = true,
                NombreFoto = productosList[0].NombreFoto,
                Platillo = new Platillo
                {
                    Receta = "e",
                }
            };

            ArticuloVentaController articuloVentaController = new ArticuloVentaController();

            ResultadoRegistro guardado = articuloVentaController.GuardarPlatilloVenta(nuevoPlatillo);

            Assert.AreEqual(guardado, ResultadoRegistro.ProductosNoEspecificados);
        }
    }
}
