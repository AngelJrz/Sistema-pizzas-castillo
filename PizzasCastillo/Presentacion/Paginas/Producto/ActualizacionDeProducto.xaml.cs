using Dominio.Enumeraciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Microsoft.Win32;
using Presentacion.Recursos;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Singleton _sesion;
        private Process _teclado;
        private const int DISPONIBLE = 1;
        private readonly List<Tipo> listaTipoProducto;
        private ValidadorProducto validadorProducto;
        private string codigoBarra = null;
        private bool tieneImagen = false;
        private byte[] foto;
        private string nombreFoto;
        private Dominio.Entidades.Producto producto;
        public ActualizacionDeProducto(Dominio.Entidades.Producto productoSeleccionado, Singleton sesion)
        {
            InitializeComponent();

            _sesion = sesion;
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

        private void ConfirmarAccion(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Confirmar Actualización", "¿Estas seguro que deseas actualizar este producto?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                ActualizarProducto();
            }
        }

        private void ActualizarProducto()
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
                        InteraccionUsuario error = new InteraccionUsuario("Error", "Ocurrió un error al intentar actualizar el producto. Por favor intente más tarde.");
                        error.Show();
                        return;
                    }

                    if (resultado)
                    {
                        InteraccionUsuario exito = new InteraccionUsuario("Exito", "Se actualizó el producto");
                        exito.Show();
                        NavigationService.Navigate(new Inicio_Gerente_Productos(_sesion));
                    }
                    else
                    {
                        InteraccionUsuario error = new InteraccionUsuario("Error", "Ocurrió un error, intenté más tarde");
                        error.Show();
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

                    InteraccionUsuario error = new InteraccionUsuario("Error", mensaje);
                    error.Show();
                }
            }
            else
            {
                InteraccionUsuario error = new InteraccionUsuario("ERROR", "No se puede actualizar un producto con estatus: Inactivo");
                error.Show();
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
