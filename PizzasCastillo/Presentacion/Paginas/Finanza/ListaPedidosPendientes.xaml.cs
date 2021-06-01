using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Dominio.Logica;
using Dominio.Entidades;
using System.Collections.ObjectModel;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para ListaPedidosPendientes.xaml
    /// </summary>
    public partial class ListaPedidosPendientes : Page
    {
        PedidoAProveedorController accesoAPedidos = new PedidoAProveedorController();
        ObservableCollection<PedidoAProveedor> listaObservable;

        public ListaPedidosPendientes()
        {
            InitializeComponent();
            listaObservable = new ObservableCollection<PedidoAProveedor>(accesoAPedidos.ObtenerPedidosAProveedores());

            if (listaObservable.Count == 0)
            {
                MessageBox.Show("No se encuentras pedidos registrados, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
            }
            else
            {
                tablaDePedidos.ItemsSource = listaObservable;
            }
        }

        private void ClickConfirmar(object sender, RoutedEventArgs e)
        {
            PedidoAProveedor pedidoSeleccioado = (PedidoAProveedor)tablaDePedidos.SelectedItem;
            NavigationService.Navigate(new ConfirmarEntrega(pedidoSeleccioado));
        }

        private void CancelarPedido(object sender, RoutedEventArgs e)
        {
            MessageBoxResult opscionSeleccionada = MessageBox.Show("Estas seguro de cancelar el pedido?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (opscionSeleccionada == MessageBoxResult.Yes) {
                PedidoAProveedor pedidoSeleccioado = (PedidoAProveedor)tablaDePedidos.SelectedItem;
                if (accesoAPedidos.CancelarPedidoAProveedor(pedidoSeleccioado))
                {
                    MessageBox.Show("El pedido se cancelo correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    listaObservable.Remove(pedidoSeleccioado);
                }
            }
            
        }

        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
