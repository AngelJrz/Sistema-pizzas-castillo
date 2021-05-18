using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para ConfirmarEntrega.xaml
    /// </summary>
    public partial class ConfirmarEntrega : Page
    {
        private List<Solicita> listaSolicitadaOriginal;
        private ObservableCollection<Solicita> listaObservable;
        private List<Dominio.Entidades.Producto> listaDeProductosEntregados;
        private PedidoAProveedor pedidoRevisado;

        public ConfirmarEntrega(PedidoAProveedor pedido)
        {
            InitializeComponent();
            listaDeProductosEntregados = new List<Dominio.Entidades.Producto>();
            listaSolicitadaOriginal = pedido.Solicita;
            pedidoRevisado = pedido;
            listaObservable = new ObservableCollection<Solicita>(listaSolicitadaOriginal);

            tablaDeProductos.ItemsSource = listaObservable;
        }

        private void ClickRegistrar(object sender, RoutedEventArgs e)
        {
            if (listaObservable.Count == 0)
            {
                MessageBoxResult resultado = MessageBox.Show("Seguro que deseas confirmar la entrega?", "confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    PedidoAProveedorController controllerPedido = new PedidoAProveedorController();
                    ProductoController controllerProducto = new ProductoController();
                    
                    if (controllerProducto.IngresarPedidoEntregado(listaDeProductosEntregados))
                    {
                        if (controllerPedido.PedidoAProveedorEntregado(pedidoRevisado))
                        {
                            MessageBox.Show("En registro del pedido se realizo correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error al actualizar el estado del pedido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al registrar el pedido, intenta de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aun cuentas con productos que registrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegistrarProductoEntregado(object sender, RoutedEventArgs e)
        {
            InputDialog cantidad = new InputDialog("Ingresa la cantidad que entrego el proveedor");
            if (cantidad.ShowDialog() == true)
            {
                if (EsNumeroValido(cantidad.ResponseText))
                {
                    Solicita productoSeleccionado = (Solicita)tablaDeProductos.SelectedItem;
                    listaDeProductosEntregados.Add(new Dominio.Entidades.Producto() {
                        CodigoBarra = productoSeleccionado.ProductoSolicitado.CodigoBarra,
                        Cantidad = productoSeleccionado.ProductoSolicitado.Cantidad + Decimal.Parse(cantidad.ResponseText)
                    });

                    listaObservable.Remove(productoSeleccionado);
                }
            }
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

        private void ClickReset(object sender, RoutedEventArgs e)
        {
            listaDeProductosEntregados = new List<Dominio.Entidades.Producto>();
            listaObservable = new ObservableCollection<Solicita>(listaSolicitadaOriginal);

            tablaDeProductos.ItemsSource = listaObservable;
        }
    }
}
