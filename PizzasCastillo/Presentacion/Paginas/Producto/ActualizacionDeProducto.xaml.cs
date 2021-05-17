using Dominio.Enumeraciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Microsoft.Win32;
using Presentacion.Ventanas;
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

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for ActualizaciónDeRegistro.xaml
    /// </summary>
    public partial class ActualizacionDeProducto : Page
    {
        private const int DISPONIBLE = 1;
        private readonly List<Tipo> listaTipoProducto;
        private ValidadorProducto validadorProducto;
        private string codigoBarra = null;
        private bool tieneImagen = false;
        private byte[] foto;
        private string nombreFoto;
        private Dominio.Entidades.Producto producto;
        public ActualizacionDeProducto(Dominio.Entidades.Producto productoSeleccionado)
        {
            InitializeComponent();

            TipoProductoController tipoProductoController = new TipoProductoController();
            listaTipoProducto = tipoProductoController.ObtenerTipoProducto();
            ListaTiposProducto.ItemsSource = listaTipoProducto;

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
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ActualizarProducto(object sender, RoutedEventArgs e)
        {
            if(producto.Estatus == DISPONIBLE)
            {
                decimal.TryParse(precioVentaText.Text, out decimal precio);
                decimal.TryParse(precioUnitarioText.Text, out decimal precioCompra);
                decimal.TryParse(cantidadText.Text, out decimal cantidad);

                Dominio.Entidades.Producto productoCambiado = new Dominio.Entidades.Producto
                {
                    CodigoBarra = codigoText.Text,
                    Nombre = NombreText.Text,
                    Precio = precio,
                    Foto = foto,
                    Estatus = producto.Estatus,
                    EsPlatillo = false,
                    NombreFoto = nombreFoto,
                    Cantidad = cantidad,
                    Descripcion = descripcionText.Text,
                    PrecioCompra = precioCompra,
                    Restricciones = restriccionesText.Text,
                    Tipo = ListaTiposProducto.SelectedItem as Tipo,
                    UnidadDeMedida = unidadMedidaText.Text
                };

                if (EstaInformacionCorrecta(productoCambiado))
                {
                    ProductoController productoController = new ProductoController();
                    bool resultado = false;
                    try
                    {
                        resultado = productoController.ActualizarProducto(productoCambiado);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ocurrió un error al intentar actualizar el producto. Por favor intente más tarde.");
                        return;
                    }

                    if (resultado)
                    {
                        MessageBox.Show("Se actualizo el producto");
                        NavigationService.Navigate(new Inicio_Gerente_Productos());
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error, intenté más tarde");
                    }
                }
                else
                {
                    List<string> camposIncorecctos = validadorProducto.ObtenerPropiedadesIncorrectas();
                    string mensaje = "Los siguientes campos están incorrectos: ";
                    foreach (var campos in camposIncorecctos)
                    {
                        mensaje += campos + ", ";
                    }

                    mensaje += "por favor verifique la información.";

                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                MessageBox.Show("eRROR: No se puede actualizar un producto con estatus :No Disponible");
            }
            
        }

        private bool EstaInformacionCorrecta(Dominio.Entidades.Producto producto)
        {
            ValidadorArticuloVenta validadorArticulo = new ValidadorArticuloVenta();
            validadorProducto = new ValidadorProducto();

            return validadorArticulo.Validar(producto) &&
                validadorProducto.Validar(producto);
        }

        private void SubirFoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                nombreFoto = op.SafeFileName;
                if (nombreFoto.Length > 150)
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error de nombre", "El nombre de esta imagen es demasiado grande, favor de reducirlo");
                    error.Show();
                }
                else
                {
                    imagenProducto.Source = new BitmapImage(new Uri(op.FileName));
                    botonImagen.Content = "Cambiar foto";
                    Stream stream = op.OpenFile();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        foto = ms.ToArray();
                    }

                    tieneImagen = true;
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
}
