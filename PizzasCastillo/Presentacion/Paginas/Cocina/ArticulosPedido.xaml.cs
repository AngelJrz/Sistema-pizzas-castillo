using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
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

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para ArticulosPedido.xaml
    /// </summary>
    public partial class ArticulosPedido : Page
    {
        private readonly int PEDIDO_PREPARADO_ESTATUS = 2;
        Dominio.Entidades.Pedido localPedido;
        public ArticulosPedido(Dominio.Entidades.Pedido pedido)
        {
            InitializeComponent(); 
            platilloList.ItemsSource = pedido.Contiene;
            localPedido = pedido;

            foreach (Contiene c in pedido.Contiene)
            {
                try
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + c.ArticuloVenta.NombreFoto, c.ArticuloVenta.Foto);

                }
                catch (System.IO.IOException)
                {

                }
            }
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void VerReceta(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)platilloList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            Contiene contiene = platilloList.SelectedItem as Contiene;
            ArticuloVenta articulo = contiene.ArticuloVenta;

            NavigationService.Navigate(new RecetaArticulo(articulo));
        }

        private void PrepararPedido(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Confirmacion",
               "¿Seguro que desea marcar este pedido como preparado?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                try
                {
                    PedidoController pedidoController = new PedidoController();
                    localPedido.Estatus.Id = PEDIDO_PREPARADO_ESTATUS;
                    pedidoController.ActualizarPedidoEstatus(localPedido);
                    InteraccionUsuario interaccionUsuario = new InteraccionUsuario("Exito de actualizacion", "Se ha actualizado el estado del pedido");
                    interaccionUsuario.Show();
                    NavigationService.Navigate(new ListaPedidosPreparar());
                }
                catch (Exception ex)
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error De Actualizacion", "Hubo un problema de comunicacion con la Base de datos, intentar mas tarde");
                    error.Show();
                }
            }
        }
    }
}
