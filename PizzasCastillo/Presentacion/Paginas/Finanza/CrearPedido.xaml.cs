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
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para CrearPedido.xaml
    /// </summary>
    public partial class CrearPedido : Page
    {
        private PedidoAProveedor nuevoPedido;
        private List<Solicita> listaDeSolicitudes = new List<Solicita>();

        public CrearPedido()
        {
            InitializeComponent();
            ProveedorController accesoProveedores = new ProveedorController();
            ArticuloVentaController accesoArticulosVenta = new ArticuloVentaController();
            
            List<Proveedor> listaDeProveedores = accesoProveedores.ObtenerProveedores();
            List<ArticuloVenta> listaDeArticulos = accesoArticulosVenta.ObtenerProductos();

            tablaDeArticulos.ItemsSource = listaDeArticulos;
            comboBoxProveedores.ItemsSource = listaDeProveedores;
            

            foreach (Proveedor proveedor in listaDeProveedores)
            {
                File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + proveedor.NombreArchivo, proveedor.ListaDeProductos);
            }
        }

        private void ClickConfirmar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show($"¿Seguro que desea realizar el pedido? \n El costo total es: ${ObtenerCostoTotal(listaDeSolicitudes)}","Confirmación",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                PedidoAProveedor nuevoPedido = ObtenerPedido();
                PedidoAProveedorController controller = new PedidoAProveedorController();

                if (controller.RegistrarNuevoPedidoAProveedor(nuevoPedido))
                {
                    MessageBox.Show("El pedido se realizo correctamente");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error durante el registro");
                }
            }
        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClickAgregar(object sender, RoutedEventArgs e)
        {
            InputDialog cantidad = new InputDialog("Ingresa la cantidad que deseas");
            if (cantidad.ShowDialog() == true)
            {
                if (EsNumeroValido(cantidad.ResponseText))
                {
                    ArticuloVenta articuloSeleccionado = (ArticuloVenta)tablaDeArticulos.SelectedItem;
                    double cantidadSolicitada = double.Parse(cantidad.ResponseText);

                    listaDeSolicitudes.Add(new Solicita() { 
                        Cantidad = new Decimal(cantidadSolicitada),
                        Precio = articuloSeleccionado.Precio,
                        ProductoSolicitado = new Dominio.Entidades.Producto() { 
                            CodigoBarra = articuloSeleccionado.CodigoBarra
                        }
                    });

                    MessageBox.Show("Solicitaste " + cantidadSolicitada + " de " + articuloSeleccionado.Nombre);
                }
            }
        }

        private PedidoAProveedor ObtenerPedido()
        {
            decimal costoTotal = ObtenerCostoTotal(listaDeSolicitudes);
            string descripcionPedido = ObtenerDescripcion();
            nuevoPedido = new PedidoAProveedor()
            {
                Descripcion = descripcionPedido,
                Fecha = DateTime.Now,
                CostoTotal = costoTotal,
                Proveedor = (Proveedor)comboBoxProveedores.SelectedItem,
                Estatus = new Dominio.Enumeraciones.Tipo()
                {
                    Id = 3
                },
                Solicita = listaDeSolicitudes
            };

            return nuevoPedido;
        }

        private string ObtenerDescripcion()
        {
            InputDialog descripcion = new InputDialog("Ingresa una descripción (Opcional)");
            if (descripcion.ShowDialog() == true)
            {
                return descripcion.ResponseText;
            }

            return "No se ingreso una descripción";
        }

        private bool EsNumeroValido(string texto)
        {
            try
            {
                double numero = double.Parse(texto.Trim());
                if (numero >= 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ingresa una cantidad valida");
                return false;
            }

            return false;
        }

        private decimal ObtenerCostoTotal(List<Solicita> listaDesolicitudes)
        {
            decimal costoTotal = 0;
            foreach (Solicita articulo in listaDesolicitudes)
            {
                costoTotal += articulo.Precio * articulo.Cantidad;
            }
            return costoTotal;
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
