using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Logica;
using Dominio.Enumeraciones;

namespace Dominio.Test.CU_MF_06
{
    [TestClass]
    public class GestionarTiposDeSalidasTest
    {
        private TipoDeGastoController _tipoGastoController;

        public GestionarTiposDeSalidasTest()
        {
            _tipoGastoController = new TipoDeGastoController();
        }

        [TestMethod]
        public void GuardarNuevoTipoDeGasto()
        {
            Tipo nuevoTipoDeGasto = new Tipo()
            {
                Nombre = "Pago a proveedor",
                Estatus = 1
            };

            bool resultado = _tipoGastoController.GuardarNuevoTipoDeGasto(nuevoTipoDeGasto);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void GuardarNuevoTipoDeGasto_InfoNula()
        {
            bool resultado = _tipoGastoController.GuardarNuevoTipoDeGasto(null);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ObtenerTiposDeGasto()
        {
            List<Tipo> resultado = _tipoGastoController.ObtenerTiposDeGasto();

            Assert.IsTrue(resultado.Count >= 0);
        }

        [TestMethod]
        public void ModificarTiposDeGasto()
        {
            string nuevoNombre = "Nombre de tipo actualizado";
            int idTipoAModificar = 1;

            bool resultado = _tipoGastoController.ModificarTiposDeGasto(idTipoAModificar, nuevoNombre);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ModificarTiposDeGasto_IDError()
        {
            string nuevoNombre = "Nombre de tipo actualizado";
            int idTipoAModificar = 234;

            bool resultado = _tipoGastoController.ModificarTiposDeGasto(idTipoAModificar, nuevoNombre);

            Assert.IsFalse(resultado);
        }
    }
}
