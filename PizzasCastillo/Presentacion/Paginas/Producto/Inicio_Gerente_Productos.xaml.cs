using AccesoADatos.ControladoresDeDatos;
using Dominio.Logica;
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
        private readonly ObservableCollection<Dominio.Entidades.Producto> productos = null;
        private Dominio.Entidades.Producto productoSeleccionado;

        public Inicio_Gerente_Productos()
        {
            InitializeComponent();

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

        }

        private void ConsultarProducto(object sender, RoutedEventArgs e)
        {

        }

        private void EditarProducto(object sender, RoutedEventArgs e)
        {
            productoSeleccionado = (Dominio.Entidades.Producto)tablaDeProductos.SelectedItem;
            NavigationService.Navigate(new ActualizacionDeProducto(productoSeleccionado));
        }

        private void EliminarProducto(object sender, RoutedEventArgs e)
        {

        }

        private void VerificarExistencia(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistroDeProducto());
        }

        private void GenerarReporteInventario(object sender, RoutedEventArgs e)
        {

        }
    }
}
