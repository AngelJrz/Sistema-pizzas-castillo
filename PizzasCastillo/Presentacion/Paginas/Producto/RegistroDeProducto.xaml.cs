
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
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Presentacion.Ventanas.Usuario;
using Dominio.Utilerias;
using Presentacion.Ventanas.Producto;
using Presentacion.Recursos;
using System.Diagnostics;

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for RegistroDeProducto.xaml
    /// </summary>
    public partial class RegistroDeProducto : Page
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

        public RegistroDeProducto(Singleton sesion)
        {
            InitializeComponent();

            _sesion = sesion;
            TipoProductoController tipoProductoController = new TipoProductoController();
            listaTipoProducto = tipoProductoController.ObtenerTipoProducto();
            ListaTiposProducto.ItemsSource = listaTipoProducto;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmarAccion(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Confirmar Registro", "¿Estas seguro que deseas registrar este producto?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                RegistrarProducto();
            }
        }

        private void RegistrarProducto()
        {
            if (tieneImagen == false)
            {
                InteraccionUsuario errorImagen = new InteraccionUsuario("ERROR","Debe de subir una foto para el producto.");
                errorImagen.Show();
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
                    Estatus = DISPONIBLE,
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
                        InteraccionUsuario error = new InteraccionUsuario("Error", "Ocurrió un error al intentar guardar el producto. Por favor intente más tarde.");
                        error.Show();
                        return;
                    }

                    if (resultado == ResultadoRegistroProducto.RegistroExitoso)
                    {
                        InteraccionUsuario exito = new InteraccionUsuario("Exito", "Se registró el producto");
                        exito.Show();
                        NavigationService.Navigate(new Inicio_Gerente_Productos(_sesion));
                    }
                    else if (resultado == ResultadoRegistroProducto.CodigoBarraDuplicado)
                    {
                        InteraccionUsuario error = new InteraccionUsuario("Error", "El codigo de barra ingresado ya pertenece a otro producto. Verifique la información e intente de nuevo");
                        error.Show();
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
                    string mensaje = "Existen campos incorrectos: ";
                    foreach (var campos in camposIncorecctos)
                    {
                        mensaje += campos + ", ";
                    }

                    mensaje += "por favor verifique la información.";
                    InteraccionUsuario error = new InteraccionUsuario("Error", mensaje);
                    error.Show();
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

            if (registroCodigoVentana.ShowDialog() == true)
            {
                codigoBarra = registroCodigoVentana.codigoIngresado;
                codigoText.Text = registroCodigoVentana.codigoIngresado;
            }   
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
