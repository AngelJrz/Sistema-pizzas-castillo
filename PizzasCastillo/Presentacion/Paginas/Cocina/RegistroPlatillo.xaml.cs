using Dominio.Entidades;
using Dominio.Logica;
using Microsoft.Win32;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
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
using System.Globalization;

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para RegistroPlatillo.xaml
    /// </summary>
    public partial class RegistroPlatillo : Page
    {
        readonly ICollectionView provedoresView;
        string imageFile;
        List<Image> imageList;
        public RegistroPlatillo()
        {
            InitializeComponent();
            ProveedorController proveedorController = new ProveedorController();
            List<Proveedor> proveedores = proveedorController.ObtenerProveedores();
            if (proveedores.Count == 0)
            {
                InteraccionUsuario ventana = new InteraccionUsuario("Error", "Aun no se encuentran registrados ningun proovedor");
                ventana.Show();
                NavigationService.GoBack();
            }
            else
            {
                provedoresView = CollectionViewSource.GetDefaultView(proveedores);
                proveedorList.ItemsSource = proveedores;
                
                /*BitmapSource bitmapSource = ConvertByteArray(Encoding.ASCII.GetBytes(proveedores.First().ListaDeProductos));
                fotoLoca.Source = bitmapSource;
                */
            }
        }

        public BitmapImage ConvertByteArray(Byte[] imageByte)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageByte))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = stream;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }

        private void agregarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                botonImagen.Content = "Cambiar foto";
            }

        }

        private void AgregarIngrediente(object sender, RoutedEventArgs e)
        {
            if (CamposVacios())
            {
                MessageBox.Show("Ingrese el nombre de la actividad y selecciona el mes correspondiente.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (YaExiste())
                {
                    MessageBox.Show("La actividad ya existe. Por favor verifique la información ingresada.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AgregarIngredienteLista();
                }
            }
            
        }

        private void AgregarIngredienteLista()
        {

        }


        private bool CamposVacios()
        {
            bool areEmpty = false;

            /*if (String.IsNullOrWhiteSpace(activityName.Text) || months.SelectedItem == null)
            {
                areEmpty = true;
            }
            */
            return areEmpty;
        }

        private bool YaExiste()
        {
            /*
            bool exist = false;

            Producto
            foreach (object activity in projectActivitiesListBox.Items)
            {
                ProjectActivity projectActivityAux = (activity as ProjectActivity);

                if (projectActivityAux.Name.Equals(activityName.Text) &&
                    projectActivityAux.Month.Equals(months.Text))
                {
                    exist = true;
                    break;
                }
            }

            return exist;
            */
            return true;
        }

    }
    public class ByteToImageConverter : IValueConverter
    {
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                //image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                //image.EndInit();
            }
            image.Freeze();
            return image;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = new BitmapImage();
            if (value != null)
            {
                img = this.ConvertByteArrayToBitMapImage(Encoding.ASCII.GetBytes(value.ToString()));
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
