using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para ListaPedidosPreparar.xaml
    /// </summary>
    public partial class ListaPedidosPreparar : Page
    {
        public ListaPedidosPreparar()
        {
            InitializeComponent();
            Dominio.Logica.PedidoController pedidoController = new Dominio.Logica.PedidoController();
            ListaPedidos.ItemsSource = pedidoController.ObtenerPedidosPreparar();
        }

        private void ChecarPedido(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.Pedido pedido = (Dominio.Entidades.Pedido)ListaPedidos.SelectedItem;
            NavigationService.Navigate(new ArticulosPedido(pedido));

        }
    }
}
