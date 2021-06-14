using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Ventanas;

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
            try
            {
                foreach (ArticuloVenta p in ListaProductos.Items)
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + p.NombreFoto, p.Foto);
                }
            }
            catch(Exception) { }
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
           ArticuloVenta productoSeleccionado = (ArticuloVenta)ListaProductos.SelectedItem;
        
        

           ValidarExistencias(productoSeleccionado);

        }

        public static void ValidarExistencias(ArticuloVenta producto) {
            if (producto.Producto.Cantidad == 0 || producto.Producto.Cantidad < 0)
            {
                InteraccionUsuario error = new InteraccionUsuario("Error de Cantidad", "ya no hay cantidad de este producto");
                error.Show();
            }
            else {
                InteraccionUsuario error = new InteraccionUsuario("Existencias validas", "Aún Existen "+producto.Producto.Cantidad+" Piezas de este producto" );
                error.Show();
            }

        
        }

      
    }
    public class ByteToImageConverter : IValueConverter
    {
        public String ConvertidorRutaImagen(string nombreArchivo)
        {
            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                return null;
            }
            return Recursos.RecursosGlobales.RUTA_IMAGENES + nombreArchivo;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string img = "";
            if (value != null)
            {
                img = this.ConvertidorRutaImagen(value.ToString());
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

     
    }

    }
