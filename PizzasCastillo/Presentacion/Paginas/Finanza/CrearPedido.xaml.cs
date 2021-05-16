using System;
using System.IO;
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
using Dominio.Entidades;
using System.Globalization;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para CrearPedido.xaml
    /// </summary>
    public partial class CrearPedido : Page
    {
        public CrearPedido()
        {
            InitializeComponent();
            ProveedorController controller = new ProveedorController();
            List<Proveedor> listaDeProveedores = controller.ObtenerProveedores();

            comboBoxProveedores.ItemsSource = listaDeProveedores;

            foreach (Proveedor proveedor in listaDeProveedores)
            {
                File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + proveedor.NombreArchivo, proveedor.ListaDeProductos);
            }
        }

        private void ClickConfirmar(object sender, RoutedEventArgs e)
        {

        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {

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
