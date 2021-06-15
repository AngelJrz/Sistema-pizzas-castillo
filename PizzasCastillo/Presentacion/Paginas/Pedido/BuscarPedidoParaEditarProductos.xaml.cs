using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Presentacion.Ventanas;
using Dominio.Logica;
using static Dominio.Enumeraciones.PedidosResult;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para BuscarPedidoParaEditarProductos.xaml
    /// </summary>
    public partial class BuscarPedidoParaEditarProductos : Page
    {
        public BuscarPedidoParaEditarProductos()
        {
            InitializeComponent();
            Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
            ListaPedidos.ItemsSource = controller.ObtenerPedidos();
            
        }

        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BusquedaText.Text))
            {
                Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
                ListaPedidos.ItemsSource = controller.ObtenerPedidos();
                InteraccionUsuario interaccion = new InteraccionUsuario("Error", "Los campos de busqueda no pueden estar vacios");
                interaccion.Show();
            }
            else 
            {
                Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
                ListaPedidos.ItemsSource = controller.ObtenerPedidosCliente(BusquedaText.Text);
                InteraccionUsuario interaccion = new InteraccionUsuario("Busqueda Realizada","Estos son los Resultados de su busqueda");
                interaccion.Show();
            }
         

        }
        private void EditarPedido(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.Pedido pedido = (Dominio.Entidades.Pedido)ListaPedidos.SelectedItem;
            if (pedido.Estatus.Nombre.Equals("En Proceso"))
            {
                NavigationService.Navigate(new Pedido.EditarPedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));
            }
            else {
                InteraccionUsuario interaccion = new InteraccionUsuario("Error", "No se puede editar un pedido en este estado");
                interaccion.Show();
            }
        }

        private void RegistrarEntrega(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.Pedido pedido = (Dominio.Entidades.Pedido)ListaPedidos.SelectedItem;
            if (pedido.Estatus.Nombre.Equals("Preparado"))
            {
                NavigationService.Navigate(new Pedido.RegistrarEntrega((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));
            }
            else
            {
                InteraccionUsuario interaccion = new InteraccionUsuario("Error", "No se puede registrar la entrega de un pedido en este estado");
                interaccion.Show();
            }
          

        }

        private void GenerarTicket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.GenerarCuentaPedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }

        private void RegistrarPago(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.RegistroDePagoDePedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));
        }

        private void CancelarPedido_Clic(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.Pedido pedidoACancelar = ListaPedidos.SelectedItem as Dominio.Entidades.Pedido;

            Confirmacion ventanaConfirmacion = new Confirmacion
            {
                Mensaje = $"¿Seguro desea cancelar el pedido con folio {pedidoACancelar.Id}?",
                Titulo = "Cancelación de pedido"
            };

            if(ventanaConfirmacion.ShowDialog() == true)
            {
                PedidoController pedidoController = new PedidoController();

                ResultsPedidos resultado;

                resultado = pedidoController.CancelarPedido(pedidoACancelar);

                switch (resultado)
                {
                    case ResultsPedidos.ActualizadoConExito:
                        new Dialog
                        {
                            Mensaje = "El pedido fue cancelado exitosamente.",
                            Titulo = "Cancelación exitosa"
                        }.ShowDialog();
                        Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
                        ListaPedidos.ItemsSource = controller.ObtenerPedidos();

                        break;
                    case ResultsPedidos.NoSePudoActualizar:
                        new Dialog
                        {
                            Mensaje = "Por el momento no se pudo cancelar el pedido. Por favor intente más tarde.",
                            Titulo = "Error de cancelación"
                        }.ShowDialog();
                        break;
                    case ResultsPedidos.CancelacionNoPermitida:
                        new Dialog
                        {
                            Mensaje = "Solo se pueden cancelar los pedidos en estado \"En Proceso\" y \"Preparado\"",
                            Titulo = "Error de cancelación"
                        }.ShowDialog();
                        break;
                }
            }
        }
    }
}
