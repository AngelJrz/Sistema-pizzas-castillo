using Dominio.Logica;
using System;
using System.Collections.Generic;
using System.Globalization;
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



namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para GenerarCuentaPedido.xaml
    /// </summary>
    public partial class GenerarCuentaPedido : Page
    {
        private Dominio.Entidades.Pedido _pedido;
        
    
        public GenerarCuentaPedido(Dominio.Entidades.Pedido pedido )
        {
           
            InitializeComponent();
            _pedido = pedido;
            EmpleadoController empleadoController = new EmpleadoController();
            List<Dominio.Entidades.Empleado> empleadoList = empleadoController.ObtenerEmpleadosActivos();
            Dominio.Entidades.Empleado empleado = empleadoList.Find(e => e.NumeroEmpleado.Equals(pedido.RegistradoPor.NumeroEmpleado));
            _pedido.RegistradoPor = empleado;
            this.DataContext = _pedido;

            ListaProductos.ItemsSource = _pedido.Contiene;

        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ImprimirTicket(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
          
        }





        private void CrearTicket() {


          
        }

      
    }





















    public class ByteToImageConvertes : IValueConverter
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


