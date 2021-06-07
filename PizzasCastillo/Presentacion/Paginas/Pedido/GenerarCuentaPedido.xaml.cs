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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using Microsoft.Win32;
using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;
using Dominio.Entidades;

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

            CrearTicket();
        }





        private void CrearTicket() {

          
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".Pdf";
            dialog.Filter = "PDF Documents (.pdf)|*.pdf";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {




                String filename = dialog.FileName;


               
                PdfWriter pw = new PdfWriter(filename);
                PdfDocument pdfDocument = new PdfDocument(pw);
                Document doc = new Document(pdfDocument, PageSize.B6);
                doc.SetMargins(0, 35, 70, 35);
                doc.Add(new iText.Layout.Element.Paragraph("Pizzas Castillo").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                doc.Add(new iText.Layout.Element.Paragraph("Direccion: " + "Calle misterios #15"));
                doc.Add(new iText.Layout.Element.Paragraph("Numero de pedido: " + _pedido.Id.ToString()));
                doc.Add(new iText.Layout.Element.Paragraph("Articulos: "));
                doc.Add(new iText.Layout.Element.Paragraph("Descripcion: "+"----------"+"Cantiddad: "+ "----------"+"Precio: " + "----------"+ "total"));
                foreach (Contiene x in _pedido.Contiene) 
                {
                    doc.Add(new iText.Layout.Element.Paragraph(x.ArticuloVenta.Nombre.ToString() + "----------"+ x.Cantidad.ToString() + "----------"+x.ArticuloVenta.Precio + "----------" + x.Total.ToString()));
                }
                doc.Add(new iText.Layout.Element.Paragraph("Fecha y hora en la que se generó: " + DateTime.Now.ToString()));
                doc.Add(new iText.Layout.Element.Paragraph("Empleado:" + _pedido.RegistradoPor.NombreCompleto));

                doc.Close();

            }



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


