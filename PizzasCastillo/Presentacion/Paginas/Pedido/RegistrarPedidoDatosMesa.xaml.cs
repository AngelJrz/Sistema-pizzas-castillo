using AccesoADatos;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Presentacion.Ventanas;
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
using static Dominio.Enumeraciones.PedidosResult;


namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para RegistrarPedidoDatosMesa.xaml
    /// </summary>
    public partial class RegistrarPedidoDatosMesa : Page
    {
        private Dominio.Entidades.Pedido _pedidoNuevo;
        private List<Tipo> _tipoPedido;
        private List<Mesa> _mesas;

        private Decimal totalPedido;
        Tipo tipoLlevar = new Tipo { Id = 1, Nombre = "Para Llevar", Estatus = 1 };
        Tipo tipoLocal = new Tipo { Id = 2, Nombre = "Local", Estatus = 1 };
        Tipo estatusEnEspera = new Tipo { Id = 1, Nombre = "En Proceso", Estatus = 1 };
        public RegistrarPedidoDatosMesa(Dominio.Entidades.Pedido pedidoNuevo)
        {
            InitializeComponent();
            _pedidoNuevo = pedidoNuevo;


            SumarTotal();

            txtEstatus.Text = estatusEnEspera.Nombre;
            MesaController controller = new MesaController();
           ComboMesa.ItemsSource = controller.ObtenetMesas(); ;
        }

        private void CancelarPedido(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Paginas.Inicio());
        }

        private void GuardarPedido(object sender, RoutedEventArgs e)
        {
            if (ComboMesa.SelectedIndex == -1)
            {


                InteraccionUsuario err = new InteraccionUsuario("error", "Faltan campos por llenar");
                err.Show();
            }
            else
            {

                _pedidoNuevo.Tipo = tipoLocal;
                _pedidoNuevo.Mesa = (Dominio.Entidades.Mesa)ComboMesa.SelectedItem;
                _pedidoNuevo.Total = totalPedido;
                _pedidoNuevo.Estatus = estatusEnEspera;

                ResultsPedidos resultado;

                PedidoController pedidoController = new PedidoController();
                resultado = (ResultsPedidos)pedidoController.AgregarPedido(_pedidoNuevo);


                if (resultado == ResultsPedidos.RegistradoConExito)

                {
                    InteraccionUsuario err = new InteraccionUsuario("Exito", "El pedido se resgistro correctamente");
                    err.Show();
                    NavigationService.Navigate(new Inicio());

                }
                else
                {
                    InteraccionUsuario err = new InteraccionUsuario("error", "El pedido no se resgistro");
                    err.Show();



                }



            }
        }








        private void SumarTotal()
        {
            foreach (Dominio.Entidades.Contiene articulo in _pedidoNuevo.Contiene)
            {
                totalPedido += articulo.Total;
                TotalText.Text = totalPedido.ToString();

            }

        }





    }
}
