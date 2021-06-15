using Dominio.Enumeraciones;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.Logica;
using static Presentacion.Recursos.PedidosResults;

namespace Dominio.Test.CU_MPE_01
{
    /// <summary>
    /// Descripción resumida de RegistrarPedidoPrueba
    /// </summary>
    [TestClass]
    public class RegistrarPedidoPrueba
    {
        public RegistrarPedidoPrueba()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
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

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ObtenerClientesTest()
        {

            ClienteController controller = new ClienteController();


            Assert.IsNotNull(controller.ObtenerPersonas());
        }


        [TestMethod]
        public void ObtenerArticulosTest()
        {

            ArticuloVentaController controller = new ArticuloVentaController();



            Assert.IsNotNull(controller.ObtenerProductos());
        }

        [TestMethod]
        public void ObtenerMesasTest()
        {


            MesaController mesaController = new MesaController();
            Assert.IsNotNull(mesaController.ObtenetMesas());
        }


        [TestMethod]
        public void ObtenerTiposPedidoTest()
        {
            TipoPedidoController controller = new TipoPedidoController();
            Assert.IsNotNull(controller.ObtenerTipoPedido());


        }


        [TestMethod]
        public void RegistrarPedido()
        {
            /*-------------------*///Crear controller Para generar pedidos.
            TipoPedidoController controller = new TipoPedidoController();
            MesaController controllerMesa = new MesaController();
            ClienteController controllerCliente = new ClienteController();
            EmpleadoController controllerEmpleado = new EmpleadoController();
            ArticuloVentaController articuloController = new ArticuloVentaController();
            PedidoController pedidocontroller = new PedidoController();
            EstatusPedidoController estatusController = new EstatusPedidoController();
           
            DateTime fecha = DateTime.Now;

            /*-------------------------------*///Armar¨Pedido
            List<Tipo> listaTipos = controller.ObtenerTipoPedido();
            List <Mesa> listMesa = controllerMesa.ObtenetMesas();
            List<Tipo> ListaEstatus = estatusController.ObtenerEstatusPedido();
            List<Empleado> empleados = controllerEmpleado.ObtenerEmpleadosActivos();
            List<Persona> cliente = controllerCliente.ObtenerPersonas();
            List<ArticuloVenta> articulos = articuloController.ObtenerProductos();
            List<Contiene> listaPedido = new List<Contiene>(); 
            Contiene cosa = new Contiene();
            List<ArticuloVenta> listaCosa = articuloController.ObtenerProductos();
            cosa.ArticuloVenta = listaCosa.Find(x => x.Nombre.Equals("Sprite"));
            cosa.Cantidad = 1;
            cosa.Total = listaCosa.Find(x => x.Nombre.Equals("Sprite")).Precio;




            Pedido pedidoGuardar = new Pedido();
            listaPedido.Add(cosa);
            pedidoGuardar.Contiene =listaPedido ;
            pedidoGuardar.Estatus = ListaEstatus.Find(x => x.Nombre.Equals("En Proceso"));
            pedidoGuardar.Fecha = fecha;
            pedidoGuardar.Mesa = listMesa.Find(x => x.Id == 1);
            pedidoGuardar.RegistradoPor = empleados.Find(x => x.Id == 2);
            pedidoGuardar.RepartidoPor = null;
            pedidoGuardar.SolicitadoPor = cliente.Find(x => x.Nombres.Equals("UsuarioL"));
            pedidoGuardar.Tipo = listaTipos.Find(x => x.Nombre.Equals("A Mesa"));
            pedidoGuardar.Total = cosa.Total;
         
            Assert.AreEqual(PedidosResult.ResultsPedidos.RegistradoConExito,pedidocontroller.AgregarPedido(pedidoGuardar));

        }








    }
}
