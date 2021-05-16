
using System;
using System.Web;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using Dominio.Enumeraciones;
using Dominio.Logica;

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for RegistroDeProducto.xaml
    /// </summary>
    public partial class RegistroDeProducto : Page
    {
        private const int ACTIVO = 1;
        private readonly List<Tipo> listaTipoProducto;
        private string codigoBarra = null;
        private bool tieneImagen = false;

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
            if(codigoBarra == null)
            {
                codigoBarra = AutogenerarCodigo();
            }

            if (!tieneImagen)
            {
                byte[] placeholder = Source = 
                byte[] imgdata = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("/Imagenes/logo.png"));
            }

            Dominio.Entidades.Producto productoNuevo = new Dominio.Entidades.Producto
            {
                CodigoBarra = codigoBarra,
                Nombre = NombreText.Text,
                Precio = precioVentaText.Text,

                Telefono = TelefonoText.Text,
                Tipo = ListaTiposProducto.SelectedItem as Tipo,
                Email = EmailText.Text,
                TipoUsuario = ListaTiposUsuario
                Username = UsernameText.Text,
                Contrasenia = PasswordText.Password,
                SalarioQuincenal = salario
            };

        }

        private void CapturarCodigo(object sender, RoutedEventArgs e)
        {
            bool seCaptura = false;
            seCaptura = MessageBox.Show

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
                imagenProducto.Source = new BitmapImage(new Uri(op.FileName));
                botonImagen.Content = "Cambiar foto";
            }
        }

        private bool EsCodigoInvalido()
        {
            bool esInvalido = false;

            String validar = academicPersonalNumber.Text;

            if (!ValidatorText.IsUserName(stringToValidate))
            {
                isWrong = true;
            }

            return isWrong;
        }
    }
}
