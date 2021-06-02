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
            NavigationService.Navigate(new Pedido.EditarPedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }

        private void RegistrarEntrega(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.RegistrarEntrega((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }

        private void GenerarTicket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.GenerarCuentaPedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }

        private void RegistrarPago(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.GenerarCuentaPedido((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }
    }
}
