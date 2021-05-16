
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using Dominio.Enumeraciones;
using Dominio.Logica;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using Dominio.Entidades;
using Presentacion.Ventanas;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Presentacion.Ventanas.Usuario;
using Dominio.Utilerias;
using Presentacion.Ventanas.Producto;

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for RegistroDeProducto.xaml
    /// </summary>
    public partial class RegistroDeProducto : Page
    {
        private const int ACTIVO = 1;
        private readonly List<Tipo> listaTipoProducto;
        private ValidadorProducto validadorProducto;
        private string codigoBarra = null;
        private bool tieneImagen = false;
        private byte[] foto;
        private string nombreFoto;

        public RegistroDeProducto()
        {
            InitializeComponent();

            TipoProductoController tipoProductoController = new TipoProductoController();
            listaTipoProducto = tipoProductoController.ObtenerTipoProducto();
            ListaTiposProducto.ItemsSource = listaTipoProducto;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            if (tieneImagen == false)
            {
                MessageBox.Show("Debe de subir una foto para el producto.");
                return;
            }
            else
            {
                decimal.TryParse(precioVentaText.Text, out decimal precio);
                decimal.TryParse(precioUnitarioText.Text, out decimal precioCompra);
                decimal.TryParse(cantidadText.Text, out decimal cantidad);

                Dominio.Entidades.Producto productoNuevo = new Dominio.Entidades.Producto
                {
                    CodigoBarra = codigoBarra,
                    Nombre = NombreText.Text,
                    Precio = precio,
                    Foto = foto,
                    Estatus = ACTIVO,
                    EsPlatillo = false,
                    NombreFoto = nombreFoto,
                    Cantidad = cantidad,
                    Descripcion = descripcionText.Text,
                    PrecioCompra = precioCompra,
                    Restricciones = restriccionesText.Text,
                    Tipo = ListaTiposProducto.SelectedItem as Tipo,
                    UnidadDeMedida = unidadMedidaText.Text
                };

                if (EstaInformacionCorrecta(productoNuevo))
                {
                    ProductoController productoController = new ProductoController();
                    ResultadoRegistroProducto resultado;
                    try
                    {
                        resultado = productoController.GuardarProducto(productoNuevo);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ocurrió un error al intentar guardar el producto. Por favor intente más tarde.");
                        return;
                    }

                    if (resultado == ResultadoRegistroProducto.RegistroExitoso)
                    {
                        MessageBox.Show("Se registró el empleado");
                        NavigationService.Navigate(new Inicio_Gerente_Productos());
                    }
                    else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
                    {
                        MessageBox.Show("El codigo de barra ingresado ya pertenece a otro producti. Verifique la información e intente de nuevo");
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
        }

        private bool EstaInformacionCorrecta(Dominio.Entidades.Producto producto)
        {
            ValidadorArticuloVenta validadorArticulo = new ValidadorArticuloVenta();
            validadorProducto = new ValidadorProducto();

            return validadorArticulo.Validar(producto) &&
                validadorProducto.Validar(producto);
        }

        private void CapturarCodigo(object sender, RoutedEventArgs e)
        {
            RegistroCodigoBarra registroCodigoVentana = new RegistroCodigoBarra();
            codigoBarra = registroCodigoVentana.codigoIngresado;
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
