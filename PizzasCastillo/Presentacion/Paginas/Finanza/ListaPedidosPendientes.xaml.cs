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
using Dominio.Logica;
using Dominio.Entidades;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para ListaPedidosPendientes.xaml
    /// </summary>
    public partial class ListaPedidosPendientes : Page
    {
        public ListaPedidosPendientes()
        {
            InitializeComponent();
            PedidoAProveedorController accesoAPedidos = new PedidoAProveedorController();
            List<PedidoAProveedor> listaDePedidos = accesoAPedidos.ObtenerPedidosAProveedores();

            tablaDePedidos.ItemsSource = listaDePedidos;
        }

        private void ClickConfirmar(object sender, RoutedEventArgs e)
        {
            PedidoAProveedor pedidoSeleccioado = (PedidoAProveedor)tablaDePedidos.SelectedItem;
            NavigationService.Navigate(new ConfirmarEntrega(pedidoSeleccioado));
        }

        private void CancelarPedido(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
