using Dominio.Entidades;
using Dominio.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dominio.Test.CU_MC_04
{
    [TestClass]
    public class GenerarMermaTest
    {
        [TestMethod]
        public void GenerarMermaPedido()
        {
            Merma nuevaMerma = new Merma
            {
                Pedido = new Dominio.Entidades.Pedido
                {
                    Id = 4
                },
                Fecha = DateTime.Now,
                Justificacion = "Se me cayeron las pizzas cuando iba a entragarlas " +
                "por que el conserje dejo mojado y mocos me cai"
            };

            MermaController mermaController = new MermaController();
            bool guardado = mermaController.GuardarMermaPedido(nuevaMerma);
            Assert.AreEqual(guardado,true);
        }

        [TestMethod]
        public void GenerarMermaPedidoFalso()
        {
            Merma nuevaMerma = new Merma
            {
                Pedido = new Dominio.Entidades.Pedido
                {
                    Id = 8
                },
                Fecha = DateTime.Now,
                Justificacion = "Se me cayeron las pizzas cuando iba a entragarlas " +
                "por que el conserje dejo mojado y mocos me cai"
            };

            MermaController mermaController = new MermaController();
            bool guardado = mermaController.GuardarMermaPedido(nuevaMerma);
            Assert.AreEqual(guardado, false);
        }

        [TestMethod]
        public void GenerarMermaInsumos()
        {
            ObservableCollection<ArticuloVenta> productos = new ObservableCollection<ArticuloVenta>();
            List<ArticuloVenta> productosList = new List<ArticuloVenta>();
            ArticuloVentaController articuloController = new ArticuloVentaController();
            List<Indica> indicaList = new List<Indica>();

            productosList = articuloController.ObtenerProductos();
            productos.Add(productosList[0]);
            productos.Add(productosList[1]);

            foreach (ArticuloVenta articulo in productos)
            {
                indicaList.Add(new Indica
                {
                    Cantidad = articulo.CantidadLocal,
                    Producto = articulo.Producto
                });
            }
            Merma nuevaMerma = new Merma
            {
                Fecha = DateTime.Now,
                Justificacion = "Estaba haciendo malabares con " +
                "los tomates y todos se me cayeron",
                Indica = indicaList
            };

            MermaController mermaController = new MermaController();
            bool guardado = mermaController.GuardarMermaInsumos(nuevaMerma);
            Assert.AreEqual(guardado, true);
        }

        [TestMethod]
        public void GenerarMermaSinInsumos()
        {
            ObservableCollection<ArticuloVenta> productos = new ObservableCollection<ArticuloVenta>();
            List<Indica> indicaList = new List<Indica>();
            Merma nuevaMerma = new Merma
            {
                Fecha = DateTime.Now,
                Justificacion = "Estaba haciendo malabares " +
                "con los tomates y todos se me cayeron",
                Indica = indicaList
            };

            MermaController mermaController = new MermaController();
            bool guardado = mermaController.GuardarMermaInsumos(nuevaMerma);
            Assert.AreEqual(guardado, false);
        }
    }
}
