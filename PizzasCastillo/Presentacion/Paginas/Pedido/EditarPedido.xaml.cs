using Dominio.Entidades;
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
using Dominio.Logica;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para EditarPedido.xaml
    /// </summary>
    public partial class EditarPedido : Page
    {
        private Dominio.Entidades.Pedido _pedido;
        public EditarPedido(Dominio.Entidades.Pedido pedidoaActualizar)
        {
            this._pedido = pedidoaActualizar;


            InitializeComponent();
            ListaProductos.ItemsSource = _pedido.Contiene;
            foreach (Contiene p in _pedido.Contiene)
            {
                File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + p.ArticuloVenta.NombreFoto, p.ArticuloVenta.Foto);
            }
        }



        private void BuscarEnter(object sender, RoutedEventArgs e)
        {

        }
        private void EliminarProducto(object sender, RoutedEventArgs e)
        {
            Contiene contieneQuitar = (Contiene)ListaProductos.SelectedItem;

            if (contieneQuitar.Cantidad == 1)
            {
                if (_pedido.Contiene.Count() >1 )
                {
                    _pedido.Contiene.Remove((Contiene)ListaProductos.SelectedItem);
                    InteraccionUsuario err = new InteraccionUsuario("Exito", "Se Retiró este producto de la lista");
                    err.Show();

                }
                else {
                    InteraccionUsuario err = new InteraccionUsuario("Error","No puedes dejar al pedido sin articulos");
                    err.Show();
                }
               
                
            }
            else {
                //Crear una lista temporal y modeificarla
            
                    _pedido.Contiene.Find(x => x.ArticuloVenta.CodigoBarra.Contains(contieneQuitar.ArticuloVenta.CodigoBarra)).Cantidad -= 1;
                    _pedido.Contiene.Find(x => x.ArticuloVenta.CodigoBarra.Contains(contieneQuitar.ArticuloVenta.CodigoBarra)).Total -= contieneQuitar.ArticuloVenta.Precio;
                InteraccionUsuario err = new InteraccionUsuario("Exito", "Se Retiró 1 en la cantidad de este producto");
                err.Show();


            }

            _pedido.Total-= contieneQuitar.ArticuloVenta.Precio;

        }
        private void GuardarEdicionProducto(object sender, RoutedEventArgs e)
        {

            PedidoController controller = new PedidoController();
            controller.ActualizarPedidoArticulos(_pedido);
        }


        
    }
    public class ByteToImageConverte : IValueConverter
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