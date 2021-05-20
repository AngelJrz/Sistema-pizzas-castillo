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

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para RegistrarEntrega.xaml
    /// </summary>
    public partial class RegistrarEntrega : Page
    {
        private Dominio.Entidades.Pedido _pedido;
        public RegistrarEntrega(Dominio.Entidades.Pedido pedido)
        {
            _pedido = pedido;
            InitializeComponent();
            TotalText.Text = _pedido.Total.ToString();
            if(_pedido.RepartidoPor != null)
            RepartidorText.Text = _pedido.RepartidoPor.Nombre;

            TipoText.Text = _pedido.Tipo.Nombre;
            if (_pedido.Mesa != null)
            MesaText.Text = _pedido.Mesa.Id.ToString();

            EstatusPedidoController controller = new EstatusPedidoController();
            ComboEstatus.ItemsSource = controller.ObtenerEstatusPedido();


        }
        private void CambiarElemento(object sender, RoutedEventArgs e)
        {
        }
        private void GuardarPedido(object sender, RoutedEventArgs e)
        {

        }


    }
}
