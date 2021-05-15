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
        private Dominio.Entidades.Empleado _empleadoEnSesion;
        public RegistrarPedidoBuscarUsuario(Dominio.Entidades.Empleado empleadoEnSesion)
        {
            //Arreglar la sesion con la clase singleton
            InitializeComponent();
            ClienteController controller = new ClienteController();
            
            ListaUsuarios.ItemsSource= controller.ObtenerClientes();
            _empleadoEnSesion = empleadoEnSesion;

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
            Dominio.Entidades.Pedido nuevoPedido = new Dominio.Entidades.Pedido();

            Dominio.Entidades.Persona clienteseleccionado = (Dominio.Entidades.Persona)ListaUsuarios.SelectedItem;
            nuevoPedido.SolicitadoPor = clienteseleccionado;
            nuevoPedido.RegistradoPor = _empleadoEnSesion;
            nuevoPedido.Fecha = DateTime.Now;
            NavigationService.Navigate(new Pedido.RegistrarPedidoArticulos(nuevoPedido));


        }


       

    }
}
