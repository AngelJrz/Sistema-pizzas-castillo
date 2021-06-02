using Dominio.Entidades;
using Presentacion.Paginas;
using Presentacion.Paginas.Finanza;
using Presentacion.Paginas.Producto;
using Presentacion.Paginas.Usuario;
using Presentacion.Recursos;
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
using System.Windows.Shapes;

namespace Presentacion.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        private Singleton _sesion;
        public VentanaPrincipal(Singleton sesion)
        {
            InitializeComponent();
            _sesion = sesion;

            _sesion.Recursos.TryGetValue("Empleado", out object empleado);

            Empleado empleadoEnSesion = empleado as Empleado;
            DataContext = empleadoEnSesion;
            MostrarMenuSuperior(empleadoEnSesion.TipoUsuario.Nombre);
        }

        private void MostrarMenuSuperior(string nombre)
        {
            switch(nombre)
            {
                case "Gerente":
                    MenuSuperiorGerente.Visibility = Visibility.Visible;
                    break;
                case "Encargado de caja":
                    MenuSuperiorEncargadoCaja.Visibility = Visibility.Visible;
                    break;
                case "Mesero":
                    MenuSuperiorMeseroCocina.Visibility = Visibility.Visible;
                    break;
                case "Cocinero":
                    MenuSuperiorMeseroCocina.Visibility = Visibility.Visible;
                    break;
                case "Contador":
                    MenuSuperiorContador.Visibility = Visibility.Visible;
                    break;
            }
        }

        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void Usuarios_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new ListaDeUsuarios());
            
        }
        private void Productos_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new Inicio_Gerente_Productos(_sesion));

        }

        private void IrAInicio_PrewiewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new Inicio());

        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            _sesion.Recursos.Clear();

            Login ventanaLogin = new Login();
            ventanaLogin.Show();
            this.Close();
        }


        private void Click_RegistrarGastoExtra(object sender, MouseButtonEventArgs e)
        {
            _sesion.Recursos.TryGetValue("Empleado", out object empleado);
            Empleado empleadoEnSesion = empleado as Empleado;

            PaginaFrame.Navigate(new RegistrarGastoExtra(empleadoEnSesion));
        }

        private void Click_CorteDeCaja(object sender, MouseButtonEventArgs e)
        {
            _sesion.Recursos.TryGetValue("Empleado", out object empleado);
            Empleado empleadoEnSesion = empleado as Empleado;

            PaginaFrame.Navigate(new CorteDeCaja(empleadoEnSesion));
        }

        private void Click_TiposDeGasto(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new TiposDeGasto());
        }

        private void Click_CrearPedidoAProveedor(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new CrearPedido());
        }

        private void Click_ConfirmarEntrega(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new ListaPedidosPendientes());
        }
    }
}
