using Dominio.Entidades;
using Dominio.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
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
using System.IO;
using System.Globalization;
using Dominio.Utilerias;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para RegistrarPedidoArticulos.xaml
    /// </summary>
    public partial class RegistrarPedidoArticulos : Page
    {
        private Dominio.Entidades.Pedido _pedidoNuevo;
        public string EstatusActivo = "Activo";

        public RegistrarPedidoArticulos(Dominio.Entidades.Pedido pedidoNuevo)
        {
            _pedidoNuevo = pedidoNuevo;
            _pedidoNuevo.Contiene = new List<Contiene>();
            InitializeComponent();
            ArticuloVentaController controller = new ArticuloVentaController();
            ListaProductos.ItemsSource = controller.ObtenerProductos();
            try
            {
                foreach (ArticuloVenta p in ListaProductos.Items)
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + p.NombreFoto, p.Foto);
        
            }
            }
            catch(IOException) 
            { 
            }
        }
        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            if (!BusquedaText.Text.Equals(string.Empty))
            {

                ArticuloVentaController controller = new ArticuloVentaController();
                ListaProductos.ItemsSource = controller.ObtenerProductosNombre(BusquedaText.Text);
                InteraccionUsuario error = new InteraccionUsuario("Exito", "BusquedaCompletada");
                error.Show();




            }
            else {
                ArticuloVentaController controller = new ArticuloVentaController();
                ListaProductos.ItemsSource = controller.ObtenerProductos();
                InteraccionUsuario error = new InteraccionUsuario("Error de busqueda", "no puede buscar con un texto vacio");
                error.Show();

            }
        }

        private void AgregarProductoPedido(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.ArticuloVenta articuloSeleccionado = (Dominio.Entidades.ArticuloVenta)ListaProductos.SelectedItem;

            Dominio.Entidades.Contiene contieneProductos = new Dominio.Entidades.Contiene();
            bool bandera = false;
           
            foreach(Dominio.Entidades.Contiene contiene in _pedidoNuevo.Contiene){
                if (contiene.ArticuloVenta == articuloSeleccionado) {
                    contiene.Cantidad ++;
                    contiene.Total += articuloSeleccionado.Precio;
                    bandera = true;
                    InteraccionUsuario agregado = new InteraccionUsuario("Agregado con Exito", "se agregó 1 mas a la cantidad del producto");
                    agregado.Show();
                    break;
                }
                

            }
            if (!bandera) {
                Dominio.Entidades.Contiene articuloAgregar = new Dominio.Entidades.Contiene();
                articuloAgregar.ArticuloVenta = articuloSeleccionado;
                articuloAgregar.Total = articuloSeleccionado.Precio;
                articuloAgregar.Cantidad = 1;
                _pedidoNuevo.Contiene.Add(articuloAgregar);
                InteraccionUsuario agregado = new InteraccionUsuario("Agregado con Exito", "se agregó el producto a la lista");
                agregado.Show();
            }
           

           
        }
        private void GuardarArticulosPedido(object sender, RoutedEventArgs e)
        {
            if (_pedidoNuevo.Contiene.Count == 0)
            {

                InteraccionUsuario agregado = new InteraccionUsuario("Error", "No se puede hacer un pedido sin agregar productos");
                agregado.Show();
            }
            else {

                if (_pedidoNuevo.Tipo.Nombre.Equals("A Mesa"))
                {

                    NavigationService.Navigate(new Pedido.RegistrarPedidoDatosMesa(_pedidoNuevo));

                }
                else {
                    NavigationService.Navigate(new Pedido.RegistrarPedidoDatos(_pedidoNuevo));

                }
               

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


