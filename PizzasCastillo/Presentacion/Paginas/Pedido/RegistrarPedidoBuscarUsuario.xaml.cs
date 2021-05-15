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
    /// Lógica de interacción para RegistrarPedido.xaml
    /// </summary>
    public partial class RegistrarPedidoBuscarUsuario : Page
    {
        public RegistrarPedidoBuscarUsuario()
        {
            InitializeComponent();
            ClienteController controller = new ClienteController();
            
            ListaUsuarios.ItemsSource= controller.ObtenerClientes();

        }

        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            ClienteController controller = new ClienteController();

            ListaUsuarios.ItemsSource = controller.ObtenerClientesNombre(BusquedaText.Text);


        }
        private void UsarClienteSinRegistro(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Usuario.RegistroCliente());
        }
        private void UsarUsuarioPedido(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
