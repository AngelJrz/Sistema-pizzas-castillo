using Dominio.Entidades;
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

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para SeleccionarTipoPedido.xaml
    /// </summary>
    public partial class SeleccionarTipoPedido : Page
    {
        
        Tipo estatusEnEspera = new Tipo { Id = 2, Nombre = "En Preparacion", Estatus = 1 };
        private List<Tipo> _tipoPedido;
        private Empleado _empleado;
        public SeleccionarTipoPedido(Empleado empleadoEnSecion)
        {
            InitializeComponent();
            TipoPedidoController controller = new TipoPedidoController();
            _tipoPedido = controller.ObtenerTipoPedido();
            ComboTipo.ItemsSource = _tipoPedido;
            _empleado = empleadoEnSecion;
        }




        private void btnRegresar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        private void btnContinuar(object sender, RoutedEventArgs e)
        {
            if (ComboTipo.SelectedIndex == -1)
            {

                InteraccionUsuario err = new InteraccionUsuario("Error", "No puede dejar los cambos vacios");
                err.Show();

            }
            else {
                Tipo tipo1 = (Tipo)ComboTipo.SelectedItem;
                if (tipo1.Nombre.Equals("A Mesa"))
                {
                    Dominio.Entidades.Pedido nuevoPedido = new Dominio.Entidades.Pedido();
                    nuevoPedido.Tipo = tipo1;
                    nuevoPedido.RegistradoPor = _empleado;
                    ClienteController controller = new ClienteController();

                    List<Persona> clientebase = controller.ObtenerClientesNombre("UsuarioL");
                    nuevoPedido.SolicitadoPor = clientebase.Find(x=>x.Nombres.Equals("UsuarioL"));
                    nuevoPedido.Fecha = DateTime.Now;
                    NavigationService.Navigate(new Pedido.RegistrarPedidoArticulos(nuevoPedido));

                }
                else {
                    Dominio.Entidades.Pedido nuevoPedido = new Dominio.Entidades.Pedido();
                    nuevoPedido.Tipo = tipo1;
                    NavigationService.Navigate(new Pedido.RegistrarPedidoBuscarUsuario(_empleado,nuevoPedido));

                }
            
            
            }
        }

    }
}
