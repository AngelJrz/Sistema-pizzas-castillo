using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Enumeraciones;
using System.Windows.Media.Imaging;
using System.IO;
using Dominio.Utilerias;

namespace Dominio.Test.CU_MPR_04
{
    /// <summary>
    /// Summary description for ReporteInventarioController
    /// </summary>
    [TestClass]
    public class GenerarRepoteInventarioTest
    {
        private EmpleadoController empleadoController;
        private ProductoController productoController;
        private ReporteInventarioController reporteController;

        public GenerarRepoteInventarioTest()
        {
            empleadoController = new EmpleadoController();
            productoController = new ProductoController();
            reporteController = new ReporteInventarioController();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void GuardarReporteValido()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach( Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad,
                    CantidadReal = 50,
                    Comentario = "Comentario Prueba"
                    
                };

                listaReportados.Add(item);
            }


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };

            bool seGuardo = reporteController.GuardarReporte(reportePrueba);
            Assert.IsTrue(seGuardo);
        }

        [TestMethod]
        public void GuardarReporteSinProductoReportados()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA",
                GeneradoPor = empleadoPrueba,
            };

            bool seGuardo = reporteController.GuardarReporte(reportePrueba);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void GuardarReporteSinNombre()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad,
                    CantidadReal = 50,
                    Comentario = "Comentario Prueba"

                };

                listaReportados.Add(item);
            }


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };

            bool seGuardo = reporteController.GuardarReporte(reportePrueba);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void GuardarReporteSinEmpleado()
        {
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad,
                    CantidadReal = 50,
                    Comentario = "Comentario Prueba"

                };

                listaReportados.Add(item);
            }


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA",
                Reporta = listaReportados
            };

            bool seGuardo = reporteController.GuardarReporte(reportePrueba);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void GuardarReporteNombreInvalido()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad,
                    CantidadReal = 50,
                    Comentario = "Comentario Prueba"

                };

                listaReportados.Add(item);
            }


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA LARGO REPORTE PRUEBA LARGO REPORTE PRUEBA LARGO REPORTE PRUEBA LARGO",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };

            bool seGuardo = reporteController.GuardarReporte(reportePrueba);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void GenerarPDFValidoDatosCompletos()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad,
                    CantidadReal = 50,
                    Comentario = "Comentario Prueba"
                };

                listaReportados.Add(item);
            }

            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA PDF",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string ruta = path + "/CU-MPR-01/ReportePrueba.pdf";
            bool seGuardo = reporteController.GenerarPDF(reportePrueba,ruta);
            Assert.IsTrue(seGuardo);
        }

        [TestMethod]
        public void GenerarPDFValidoDatosParciales()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad
                };

                listaReportados.Add(item);
            }


            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA PDF 2",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string ruta = path + "/CU-MPR-01/ReportePrueba2.pdf";
            bool seGuardo = reporteController.GenerarPDF(reportePrueba, ruta);
            Assert.IsTrue(seGuardo);
        }

        [TestMethod]
        public void GenerarPDFInvalidoSinReporta()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();

            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA INVALIDA SIN REPORTA",
                GeneradoPor = empleadoPrueba,
            };
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string ruta = path + "/CU-MPR-01/ReportePrueba2.pdf";
            bool seGuardo = reporteController.GenerarPDF(reportePrueba, ruta);
            Assert.IsFalse(seGuardo);
        }

        [TestMethod]
        public void GenerarPDFInvalidoSinRuta()
        {
            List<Empleado> listaEmpleados = empleadoController.ObtenerEmpleadosActivos();
            Empleado empleadoPrueba = listaEmpleados.Find(e => e.TipoUsuario.Nombre.Equals("Gerente"));
            List<Producto> listaProductos = productoController.ObtenerProductosActivos();
            List<Reporta> listaReportados = new List<Reporta>();

            foreach (Producto p in listaProductos)
            {
                Reporta item = new Dominio.Entidades.Reporta
                {
                    Producto = p,
                    CantidadEnInventario = p.Cantidad
                };

                listaReportados.Add(item);
            }

            ReporteInventario reportePrueba = new ReporteInventario
            {
                Nombre = "REPORTE PRUEBA INVALIDO",
                GeneradoPor = empleadoPrueba,
                Reporta = listaReportados
            };

            string ruta = "";
            bool seGuardo = reporteController.GenerarPDF(reportePrueba, ruta);
            Assert.IsFalse(seGuardo);
        }
    }
}
