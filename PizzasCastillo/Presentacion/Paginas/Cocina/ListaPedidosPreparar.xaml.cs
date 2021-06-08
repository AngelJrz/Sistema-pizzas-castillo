using Dominio.Entidades;
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
