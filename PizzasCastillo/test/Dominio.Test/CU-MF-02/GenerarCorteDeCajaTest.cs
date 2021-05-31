using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;
using System.Collections.Generic;

namespace Dominio.Test.CU_MF_02
{
    [TestClass]
    public class GenerarCorteDeCajaTest
    {
        private ReporteDeCajaController _corteCajaController;

        public GenerarCorteDeCajaTest()
        {
            _corteCajaController = new ReporteDeCajaController();
        }

        [TestMethod]
        public void RegistrarReporteDeCaja()
        {
            Efectivo billeteQuinientos = new Efectivo()
            {
                Id = 6,
                Valor = 500,
                Tipo = "billeteQuinientos"
            };

            Efectivo billeteDocientos = new Efectivo()
            {
                Id = 7,
                Valor = 200,
                Tipo = "billeteDocientos"
            };

            Guarda guardadoUno = new Guarda()
            {
                Cantidad = 2,
                Efectivo = billeteDocientos
            };

            Guarda guardadoDos = new Guarda()
            {
                Cantidad = 2,
                Efectivo = billeteQuinientos
            };

            List<Guarda> listaDeEfectivo = new List<Guarda>();
            listaDeEfectivo.Add(guardadoUno);
            listaDeEfectivo.Add(guardadoDos);

            ReporteCaja nuevoReporte = new ReporteCaja()
            {
                TotalEntrada = new Decimal(1400),
                TotalSalida = new Decimal(700),
                BalanceDiario = new Decimal(700),
                Fecha = DateTime.Now,
                TotalEfectivoContado = 1400,
                Observaciones = "El cierre de caja se hizo correctamente",
                EfectivoDiaSiguiente = new decimal(300),
                GeneradoPor = new Empleado()
                {
                    NumeroEmpleado = "2"
                },
                Guarda = listaDeEfectivo
            };
        
            bool resultado = _corteCajaController.RegistrarNuevoReporteDeCaja(nuevoReporte);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void RegistrarReporteDeCaja_NumeroEmpleadoError()
        {
            Efectivo billeteQuinientos = new Efectivo()
            {
                Id = 6,
                Valor = 500,
                Tipo = "billeteQuinientos"
            };

            Efectivo billeteDocientos = new Efectivo()
            {
                Id = 7,
                Valor = 200,
                Tipo = "billeteDocientos"
            };

            Guarda guardadoUno = new Guarda()
            {
                Cantidad = 2,
                Efectivo = billeteDocientos
            };

            Guarda guardadoDos = new Guarda()
            {
                Cantidad = 2,
                Efectivo = billeteQuinientos
            };

            List<Guarda> listaDeEfectivo = new List<Guarda>();
            listaDeEfectivo.Add(guardadoUno);
            listaDeEfectivo.Add(guardadoDos);

            ReporteCaja nuevoReporte = new ReporteCaja()
            {
                TotalEntrada = new Decimal(1400),
                TotalSalida = new Decimal(700),
                BalanceDiario = new Decimal(700),
                Fecha = DateTime.Now,
                TotalEfectivoContado = 1400,
                Observaciones = "El cierre de caja se hizo correctamente",
                EfectivoDiaSiguiente = new decimal(300),
                GeneradoPor = new Empleado()
                {
                    NumeroEmpleado = "234"
                },
                Guarda = listaDeEfectivo
            };

            bool resultado = _corteCajaController.RegistrarNuevoReporteDeCaja(nuevoReporte);

            Assert.IsFalse(resultado);
        }
    }
}
