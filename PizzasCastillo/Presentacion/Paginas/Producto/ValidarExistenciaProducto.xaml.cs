using System;
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
        }
        private void BuscarEnter(object sender, RoutedEventArgs e)
        {

        }



        private void ConsultarProducto(object sender, RoutedEventArgs e)
        {

        }

        private void ValidarExistencias(object sender, RoutedEventArgs e)
        {

        }
       

    }



    public class Producto
    {
        public string codigoDeBarras {get;set;}
        public string estatus { get; set; }
        public string foto { get; set; }
        public string nombre { get; set; }
        public float presio { get; set; }
        public string descripcion { get; set; }
        public float preciounitario { get; set; }
        public string restricciones { get; set; }
        public string Tipo { get; set; }
        public string unidadDeMedida { get; set; }
    }
}
