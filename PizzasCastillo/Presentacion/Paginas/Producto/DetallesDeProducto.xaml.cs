using Dominio.Logica;
using System;
using System.Collections.Generic;
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

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for DetallesDeProducto.xaml
    /// </summary>
    public partial class DetallesDeProducto : Page
    {
        private const int DISPONIBLE = 1;
        private Dominio.Entidades.Producto producto;

        public DetallesDeProducto(Dominio.Entidades.Producto productoSeleccionado)
        {
            InitializeComponent();

            var photoBitmap = new BitmapImage();

            using (var stream = new MemoryStream(productoSeleccionado.Foto))
            {
                photoBitmap.BeginInit();
                photoBitmap.CacheOption = BitmapCacheOption.OnLoad;
                photoBitmap.StreamSource = stream;
                photoBitmap.EndInit();
            }

            imagenProducto.Source = photoBitmap;
            producto = productoSeleccionado;

            this.DataContext = producto;
            if (producto.Estatus == DISPONIBLE)
                estatusText.Text = "Disponible";
            else estatusText.Text = "Inactivo";

            ListaTiposProducto.Text = producto.Tipo.Nombre;

            this.DataContext = productoSeleccionado;
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
