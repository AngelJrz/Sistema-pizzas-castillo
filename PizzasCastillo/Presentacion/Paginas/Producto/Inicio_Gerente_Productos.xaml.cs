using AccesoADatos.ControladoresDeDatos;
using Dominio.Logica;
using Presentacion.Recursos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Dominio.Entidades.Producto productoSeleccionado;
        private readonly List<string> filtrosLista;
        private readonly Singleton _sesion;

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

            ProductoController productoController = new ProductoController();
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
            ProductoController productoController = new ProductoController();
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
                        listaDeProductos = productoController.BuscarProductosPorNombre(busqueda);                    }
                }
            }

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

        private void EliminarProducto(object sender, RoutedEventArgs e)
        {
            bool seElimino = false;

            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            ProductoController productoController = new ProductoController();

            seElimino = productoController.EliminarProducto(productoSeleccionado);

            if (seElimino)
            {
                MessageBox.Show("Eliminación Exitosa");
            }
            else
            {
                MessageBox.Show("Ocurrió un error, intenté más tarde");
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
    }
}
