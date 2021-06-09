using AccesoADatos.ControladoresDeDatos;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Presentacion.Recursos;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for Inicio_Gerente_Productos.xaml
    /// </summary>
    public partial class Inicio_Gerente_Productos : Page
    {
        private ObservableCollection<Dominio.Entidades.Producto> productos = null;
        private ProductoController productoController = new ProductoController();
        private Dominio.Entidades.Producto productoSeleccionado;
        private readonly List<string> filtrosLista;
        private readonly Singleton _sesion;
        private Process _teclado;
        private const int NO_DISPONIBLE = 2;

        public Inicio_Gerente_Productos(Singleton sesion)
        {
            InitializeComponent();

            _sesion = sesion;

            filtrosLista = new List<string>
            {
                "Código de Barra",
                "Nombre"
            };

            TipoBusqueda.ItemsSource = filtrosLista;
            TipoBusqueda.SelectedItem = filtrosLista.Find(f => f.Contains("Nombre"));
            List<Dominio.Entidades.Producto> listaDeProductos = productoController.ObtenerProductos();

            if (listaDeProductos.Count == 0)
            {
                MessageBox.Show("No existen Productos. Debe crear un Producto primero");
            }
            else
            {
                productos = new ObservableCollection<Dominio.Entidades.Producto>(listaDeProductos);
                tablaDeProductos.ItemsSource = productos;
            }
        }

        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            string busqueda = BusquedaText.Text;
            List<Dominio.Entidades.Producto> listaDeProductos = new List<Dominio.Entidades.Producto>();
            string filtroSeleccionado = (string)TipoBusqueda.SelectedItem;

            if (String.IsNullOrWhiteSpace(busqueda) || TipoBusqueda.SelectedItem == null)
            {
                listaDeProductos = productoController.ObtenerProductos();
            }
            else
            {
                if (filtroSeleccionado.Equals("Código de Barra"))
                {
                    listaDeProductos = productoController.BuscarProductosPorCodigo(busqueda);
                }
                else
                {
                    if (filtroSeleccionado.Equals("Nombre"))
                    {
                        listaDeProductos = productoController.BuscarProductosPorNombre(busqueda);    
                    }
                }
            }

            productos = new ObservableCollection<Dominio.Entidades.Producto>(listaDeProductos);
            tablaDeProductos.ItemsSource = productos;
        }

        private void RefrescarTabla()
        {
            List<Dominio.Entidades.Producto> listaDeProductos = new List<Dominio.Entidades.Producto>();
            listaDeProductos = productoController.ObtenerProductos();
            productos = new ObservableCollection<Dominio.Entidades.Producto>(listaDeProductos);
            tablaDeProductos.ItemsSource = productos;
        }

        private void ConsultarProducto(object sender, RoutedEventArgs e)
        {
            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            NavigationService.Navigate(new DetallesDeProducto(productoSeleccionado));
        }

        private void EditarProducto(object sender, RoutedEventArgs e)
        {
            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            NavigationService.Navigate(new ActualizacionDeProducto(productoSeleccionado, _sesion));
        }

        private void ConfirmarEliminar(object sender, RoutedEventArgs e)
        {
            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            Confirmacion dialogoConfirmacion = new Confirmacion("Confirmar Eliminación",
                "¿Estas seguro que deseas eliminar este producto: " + productoSeleccionado.Nombre + "?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                EliminarProducto();
            }
        }
        private void EliminarProducto()
        {
            ResultadoEliminarProducto resultado;
            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            resultado = productoController.EliminarProducto(productoSeleccionado);

            if (resultado == ResultadoEliminarProducto.BajaExitosa)
            {
                InteraccionUsuario exito = new InteraccionUsuario("Exito", "Baja exitosa.");
                exito.Show();
                RefrescarTabla();
            }
            else
            {
                if(resultado == ResultadoEliminarProducto.BajaInvalida)
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error", "Ya se dio de baja este producto.");
                    error.Show();
                }
                else
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error", "No se pudo eliminar el producto. Intente de nuevo más tarde.");
                    error.Show();
                }
            }
        }

        private void VerificarExistencia(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ValidarExistenciaProducto());
        }

        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistroDeProducto(_sesion));
        }

        private void GenerarReporteInventario(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GeneraciónDeReporteInventario(_sesion));
        }

        private void AbrirTeclado_Touch(object sender, TouchEventArgs e)
        {
            _teclado = Process.Start("osk.exe");

            if (sender.GetType() == typeof(TextBox))
            {
                ((TextBox)sender).Focus();
            }
            else
            {
                ((PasswordBox)sender).Focus();
            }
        }

        private void CerrarTeclado(object sender, RoutedEventArgs e)
        {
            if (_teclado != null)
            {
                if (!_teclado.HasExited)
                    _teclado.Kill();
            }
        }
    }
}
