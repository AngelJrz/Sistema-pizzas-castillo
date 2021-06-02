using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Dominio.Logica;
using Presentacion.Ventanas;
using static Presentacion.Recursos.PedidosResults;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Interaction logic for RegistroDePagoDePedido.xaml
    /// </summary>
    public partial class RegistroDePagoDePedido : Page
    {
        private Dominio.Entidades.Pedido _pedido;

        public RegistroDePagoDePedido(Dominio.Entidades.Pedido pedido)
        {
            InitializeComponent();

            _pedido = pedido;
            this.DataContext = _pedido;
            //precioText.Text = _pedido.Total.ToString();

            //TipoText.Text = _pedido.Tipo.Nombre;

            
        }

        private void RegistrarPago(object sender, RoutedEventArgs e)
        {
            EstatusPedidoController estatusController = new EstatusPedidoController();
            List<Dominio.Enumeraciones.Tipo> listaTipos = estatusController.ObtenerEstatusPedido();
            _pedido.Estatus = listaTipos.Find(t => t.Nombre.Equals("Pagado"));

            PedidoController pedidoController = new PedidoController();
            ResultsPedidos resultado;
            resultado = (ResultsPedidos)pedidoController.ActualizarPedidoEstatus(_pedido);


            if (resultado == ResultsPedidos.ActualizadoConExito)

            {
                InteraccionUsuario exito = new InteraccionUsuario("Exito", "Se actualizo a Pagado el Pedido");
                exito.Show();

            }
            else
            {
                InteraccionUsuario error = new InteraccionUsuario("error", "El pedido no se no se actualizo a Pagado");
                error.Show();
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
