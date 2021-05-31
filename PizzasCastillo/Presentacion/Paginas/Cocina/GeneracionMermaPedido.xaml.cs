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
    /// Lógica de interacción para GeneracionMermaPedido.xaml
    /// </summary>
    public partial class GeneracionMermaPedido : Page
    {
        public GeneracionMermaPedido()
        {
            InitializeComponent();
            Dominio.Logica.PedidoController pedidoController = new Dominio.Logica.PedidoController();
            ListaPedidos.ItemsSource = pedidoController.ObtenerPedidosHoy();
        }

        private void BuscarEnter(object sender, RoutedEventArgs e)
        {

        }
        private void GenerarMerma(object sender, RoutedEventArgs e)
        {
            List<Contiene> contieneList = new List<Contiene>();
            Dominio.Entidades.Pedido pedido = (Dominio.Entidades.Pedido)ListaPedidos.SelectedItem;
            contieneList = pedido.Contiene;
            NavigationService.Navigate(new ArticulosPedidoMerma(contieneList, pedido.Id));

        }
    }
}
