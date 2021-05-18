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
    /// Lógica de interacción para EditarPedido.xaml
    /// </summary>
    public partial class EditarPedido : Page
    {
        private Dominio.Entidades.Pedido pedido;
        public EditarPedido(Dominio.Entidades.Pedido pedidoaActualizar)
        {
            this.pedido = pedidoaActualizar;

            InitializeComponent();
        }



        private void BuscarEnter(object sender, RoutedEventArgs e)
        {

        }
        private void EliminarProducto(object sender, RoutedEventArgs e)
        {

        }
        private void GuardarEdicionProducto(object sender, RoutedEventArgs e)
        {

        }
    }
}
