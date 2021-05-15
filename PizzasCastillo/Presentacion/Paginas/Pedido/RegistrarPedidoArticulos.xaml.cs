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

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para RegistrarPedidoArticulos.xaml
    /// </summary>
    public partial class RegistrarPedidoArticulos : Page
    {
        private Dominio.Entidades.Pedido _pedidoNuevo;
       
        public RegistrarPedidoArticulos(Dominio.Entidades.Pedido pedidoNuevo)
        {
            _pedidoNuevo = pedidoNuevo;
            InitializeComponent();
            Dominio.Logica.ProductoController controller = new Dominio.Logica.ProductoController();
            ListaProductos.ItemsSource=controller.ObtenerProductos();
        }
        private void BuscarEnter(object sender, RoutedEventArgs e)
        {
            

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
                    break;
                }
                

            }
            if (!bandera) {
                Dominio.Entidades.Contiene articuloAgregar = new Dominio.Entidades.Contiene();
                articuloAgregar.ArticuloVenta = articuloSeleccionado;
                articuloAgregar.Total = articuloSeleccionado.Precio;
                articuloAgregar.Cantidad = 1;
                _pedidoNuevo.Contiene.Add(articuloAgregar);
            
            }
           

           
        }
        private void GuardarArticulosPedido(object sender, RoutedEventArgs e)
        { 
        
        
        }


            


        /*

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public Persona SolicitadoPor { get; set; }
        public Tipo Estatus { get; set; }
        public Empleado RegistradoPor { get; set; }
        public Tipo Tipo { get; set; }
        public Repartidor RepartidoPor { get; set; }
        public Mesa Mesa { get; set; }
        public List<Contiene> Contiene { get; set; }

        */

    }
}
