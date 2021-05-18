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
    /// Lógica de interacción para RegistrarPedidoDatos.xaml
    /// </summary>
    public partial class RegistrarPedidoDatos : Page
    {
        private Dominio.Entidades.Pedido _pedidoNuevo;
        private List<AccesoADatos.EstatusPedido> _estatus;
        private List<String> nombreEstatus;
        private Decimal totalPedido;

        public RegistrarPedidoDatos(Dominio.Entidades.Pedido pedidoNuevo)
        {

            _pedidoNuevo = pedidoNuevo;
            SumarTotal();
            InitializeComponent();
            Dominio.Logica.EstatusPedidoController controller = new Dominio.Logica.EstatusPedidoController();
            _estatus = controller.ObtenerEstatusPedido();
            foreach (AccesoADatos.EstatusPedido nombre in _estatus) {
                nombreEstatus.Add(nombre.Nombre);
            
            }
            ComboEstatus.ItemsSource = nombreEstatus;
            
        }
        //AgregarEstatusPedido
        private void GuardarPedido(object sender, RoutedEventArgs e)
        {
          
                RepartidorText.IsEnabled = false;
                Dominio.Enumeraciones.Tipo tipo = new Dominio.Enumeraciones.Tipo();


                AccesoADatos.EstatusPedido estatusSeleccionado = _estatus[ComboEstatus.SelectedIndex];
                Dominio.Enumeraciones.Tipo tipoClone = new Dominio.Enumeraciones.Tipo();
                tipoClone.Estatus = estatusSeleccionado.Estatus;
                tipoClone.Id = estatusSeleccionado.Id;
                tipoClone.Nombre = estatusSeleccionado.Nombre;

                _pedidoNuevo.Estatus = tipoClone;
                _pedidoNuevo.Total = totalPedido;
                _pedidoNuevo.Tipo = tipoClone;//Esto es estatus, arreglar

               // _pedidoNuevo.Mesa = ComboMesa.Text;//Tiene que ser objeto mesa
                _pedidoNuevo.RepartidoPor = null;






                Dominio.Logica.PedidoController controller = new Dominio.Logica.PedidoController();
                controller.AgregarPedido(_pedidoNuevo);

          
                
                

          
          
            //agregar checkbox para saber que tipo de pedido es.
            //si selecciona el de mesa activa el combo de mesa, si activa enviar, activa el textbox de idRepartidor
            
        }


        private void SumarTotal() {
            foreach (Dominio.Entidades.Contiene articulo in _pedidoNuevo.Contiene) {
                totalPedido += articulo.Total;
            
            }
        
        }
    }
}
