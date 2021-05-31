using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;

namespace Dominio.Test.CU_MF_01
{
    [TestClass]
    public class RegistrarGastoExtraTest
    {
        private GastoExtraController _gastoController;

        public RegistrarGastoExtraTest()
        {
            _gastoController = new GastoExtraController();
        }

        [TestMethod]
        public void RegistrarGastoExtra()
        {
            GastoExtra nuevoGasto = new GastoExtra()
            {
                Comentario = "Pago de pedido a proveedor",
                Total = new Decimal(500),
                Fecha = DateTime.Now,
                Tipo = new Tipo()
                {
                    Id = 1
                },
                RegistradoPor = new Empleado()
                {
                    NumeroEmpleado = "2"
                }
            };

            bool resultado = _gastoController.RegistrarGastoExtra(nuevoGasto);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void RegistrarGastoExtra_EmpleadoInvalido()
        {
            GastoExtra nuevoGasto = new GastoExtra()
            {
                Comentario = "Pago de pedido a proveedor",
                Total = new Decimal(500),
                Fecha = DateTime.Now,
                Tipo = new Tipo()
                {
                    Id = 1
                },
                RegistradoPor = new Empleado()
                {
                    NumeroEmpleado = "453"
                }
            };

            bool resultado = _gastoController.RegistrarGastoExtra(nuevoGasto);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ObtenerSumaDeGastosDelDia()
        {
            double resultado = _gastoController.ObtenerSumaDeGastosDelDia();

            Assert.IsTrue(resultado >= 0);
        }
    }
}