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
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Lógica de interacción para RegistrarPedidoDatos.xaml
    /// </summary>
    public partial class RegistrarPedidoDatos : Page
    {
        private Dominio.Entidades.Pedido _pedidoNuevo;
        private List<Tipo> _tipoPedido;
        private List<Mesa> _mesas;

        private Decimal totalPedido;
        Tipo tipoLlevar = new Tipo { Id = 1,Nombre = "Para Llevar",Estatus = 1};
        Tipo tipoLocal = new Tipo { Id = 2, Nombre = "Local", Estatus = 1 };
        Tipo estatusEnEspera = new Tipo { Id = 1, Nombre = "En Espera", Estatus = 1 };
    

        public RegistrarPedidoDatos(Dominio.Entidades.Pedido pedidoNuevo)
        {

            
            InitializeComponent();
            _pedidoNuevo = pedidoNuevo;
            MesaController mesaController = new MesaController();
            _mesas = mesaController.ObtenetMesas();
            ComboMesa.ItemsSource = _mesas;

            SumarTotal();
            TipoPedidoController controller = new TipoPedidoController();
            _tipoPedido = controller.ObtenerTipoPedido();
            ComboTipo.ItemsSource = _tipoPedido;
            txtEstatus.Text = estatusEnEspera.Nombre;
          
          
            
        }
        //AgregarEstatusPedido
        private void CambiarElemento(object sender, RoutedEventArgs e)
        {
            if (ComboTipo.SelectedIndex == 0)
            {
                ComboMesa.IsEnabled = false;
                RepartidorText.IsEnabled = true;

            }
            else if (ComboTipo.SelectedIndex == 1) {
                RepartidorText.IsEnabled = false;
                ComboMesa.IsEnabled = true;
            }
            
        
        
        }
        
        private void GuardarPedido(object sender, RoutedEventArgs e)
        {
            if (ComboTipo.SelectedIndex == 0)
            {
                _pedidoNuevo.Tipo = tipoLlevar;
                
               

            }
            else if (ComboTipo.SelectedIndex == 1)
            {
                _pedidoNuevo.Tipo = tipoLocal;
                _pedidoNuevo.Mesa = (Mesa)ComboMesa.SelectedItem;
                _pedidoNuevo.Total = totalPedido;

            }












        }


        private void SumarTotal() {
            foreach (Dominio.Entidades.Contiene articulo in _pedidoNuevo.Contiene) {
                totalPedido += articulo.Total;
                TotalText.Text = totalPedido.ToString();
            
            }
        
        }
    }
}
