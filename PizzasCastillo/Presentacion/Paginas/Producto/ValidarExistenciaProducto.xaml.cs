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

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Lógica de interacción para ValidarExistenciaProducto.xaml
    /// </summary>
    public partial class ValidarExistenciaProducto : Page
    {
        
        public ValidarExistenciaProducto()
        {
            InitializeComponent();
            ArticuloVentaController controller = new ArticuloVentaController();
            ListaProductos.ItemsSource = controller.ObtenerProductos();
        }
        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            ArticuloVentaController controller = new ArticuloVentaController();
            //ListaProductos.ItemsSource=controller.BuscarProductosNombre(BusquedaText.Text);

        }

      


        private void ConsultarProducto(object sender, RoutedEventArgs e)
        {

        }

        private void ValidarExistencias(object sender, RoutedEventArgs e)
        {
            AccesoADatos.Producto productoSeleccionado = (AccesoADatos.Producto)ListaProductos.SelectedItem;
          ArticuloVentaController controller = new ArticuloVentaController();
             //AccesoADatos.Producto productoValidar = controller.BuscarProductoID(productoSeleccionado.CodigoBarra);

            //ValidarExistencias(productoValidar);

        }

        public static void ValidarExistencias(AccesoADatos.Producto producto) {
            if (producto.Cantidad == 0 || producto.Cantidad < 0)
            {
                MessageBox.Show("ya no hay existencia de este producto");
            }
            else {
                MessageBox.Show("Aun hay existen " + producto.Cantidad + " de este producto");
            }

        
        }

      
    }



   
}
