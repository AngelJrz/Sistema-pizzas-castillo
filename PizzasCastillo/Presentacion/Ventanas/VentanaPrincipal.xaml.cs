using Dominio.Entidades;
using Presentacion.Paginas;
using Presentacion.Paginas.Finanza;
using Presentacion.Paginas.Producto;
using Presentacion.Paginas.Usuario;
using Presentacion.Paginas.Cocina;
using Presentacion.Paginas.Pedido;
using Presentacion.Recursos;
using System;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace Presentacion.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        private Singleton _sesion;
        public bool IsClosed { get; private set; }
        public VentanaPrincipal(Singleton sesion)
        {
            InitializeComponent();
            _sesion = sesion;

            _sesion.Recursos.TryGetValue("Empleado", out object empleado);

            Empleado empleadoEnSesion = empleado as Empleado;
            DataContext = empleadoEnSesion;
            MostrarMenuSuperior(empleadoEnSesion.TipoUsuario.Nombre);
            try
            {
                if (Directory.Exists(Recursos.RecursosGlobales.RUTA_IMAGENES))
                {
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(Recursos.RecursosGlobales.RUTA_IMAGENES);

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
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
                    MenuSuperiorCocinero.Visibility = Visibility.Visible;
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
            
            string[] files = Directory.GetFiles(Recursos.RecursosGlobales.RUTA_IMAGENES);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (IOException)
                {
                }
                
            }

            try
            {
                Directory.Delete(Recursos.RecursosGlobales.RUTA_IMAGENES);
            }
            catch (IOException) { }
        }

        private void Usuarios_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new ListaDeUsuarios());
            
        }
        private void Productos_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new Inicio_Gerente_Productos(_sesion));

        }

        private void CocineroPlatillos_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new MenuPlatillos());

        }
        private void CocineroPedidos_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new ListaPedidosPreparar());

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
            _sesion.Recursos.TryGetValue("Empleado", out object empleado);
            Empleado empleadoEnSesion = empleado as Empleado;

            PaginaFrame.Navigate(new ListaPedidosPendientes(empleadoEnSesion));
        }
        private void Pedidos(object sender, MouseButtonEventArgs e)
        {
        

            PaginaFrame.Navigate(new BuscarPedidoParaEditarProductos());
        }

        private void RegistrarPedidos(object sender, MouseButtonEventArgs e)
        {
            _sesion.Recursos.TryGetValue("Empleado", out object empleado);
            Empleado empleadoEnSesion = empleado as Empleado;

            PaginaFrame.Navigate(new SeleccionarTipoPedido(empleadoEnSesion));
        }
        
        private void Merma_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new MenuMerma());
        }

        private void RegistroClientes_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new RegistroCliente());
        }
        private void Proveedores_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PaginaFrame.Navigate(new MenuProveedores());
        }
        
    }
}
