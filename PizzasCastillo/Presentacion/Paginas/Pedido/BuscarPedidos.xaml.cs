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

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para BuscarPedidos.xaml
    /// </summary>
    public partial class BuscarPedidos : Page
    {
        public BuscarPedidos()
        {
            InitializeComponent();
            Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
            ListaPedidos.ItemsSource = controller.ObtenerPedidos();
        }

        private void BuscarEnter(object sender, RoutedEventArgs e)
        {

        }
        private void RegistrarEntrega(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pedido.RegistrarEntrega((Dominio.Entidades.Pedido)ListaPedidos.SelectedItem));

        }
        private void EditarPedido(object sender, RoutedEventArgs e)
        {
        }

    }
}

