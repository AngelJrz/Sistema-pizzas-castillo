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
using Dominio.Enumeraciones;
using Dominio.Logica;
using Presentacion.Ventanas;
using Presentacion.Ventanas.Usuario;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para TiposUsuario.xaml
    /// </summary>
    public partial class TiposUsuario : Page
    {
        private ObservableCollection<Tipo> _tiposUsuario;
        private TipoUsuarioController _tipoUsuarioController;
        private RegistroTipoUsuario ventanaRegistroTipoUsuario;
        private EditarTipoUsuario ventanaEditar;

        public TiposUsuario()
        {
            InitializeComponent();

            //_tipoUsuarioController = new TipoUsuarioController();
            CargarTiposUsuarios();
        }

        private void CargarTiposUsuarios()
        {
            _tipoUsuarioController = new TipoUsuarioController();
            List<Tipo> tiposUsuariosConsultados = _tipoUsuarioController.ObtenerTiposUsuario();
            _tiposUsuario = new ObservableCollection<Tipo>(tiposUsuariosConsultados);
            ListaTiposUsuario.ItemsSource = _tiposUsuario;
            ListaTiposUsuario.ItemsSource = tiposUsuariosConsultados;
        }

        private void BuscarEnter(object sender, KeyEventArgs e)
        {

        }

        private void RealizarBusqueda()
        {

        }
        private void ConsultarTipoUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EditarTipoUsuario(object sender, RoutedEventArgs e)
        {
            Tipo tipoSeleccionado = ListaTiposUsuario.SelectedItem as Tipo;
            Tipo tipoUsuarioAEditar = new Tipo
            {
                Id = tipoSeleccionado.Id,
                Nombre = tipoSeleccionado.Nombre,
                Estatus = tipoSeleccionado.Estatus
            };

            if (tipoUsuarioAEditar != null)
            {
                ventanaEditar = new EditarTipoUsuario(tipoUsuarioAEditar);
                //ventanaEditar.ActualizacionExitosa += new EventHandler(TipoUsuarioEditado);

                //ventanaEditar.ShowDialog();

                if (ventanaEditar.ShowDialog() == true)
                    CargarTiposUsuarios();
            }
        }

        //private void TipoUsuarioEditado(object sender, EventArgs e)
        //{
        //    CargarTiposUsuarios();
        //}

        private void EliminarTipoUsuario(object sender, RoutedEventArgs e)
        {
            Tipo tipoUsuarioAEliminar = ListaTiposUsuario.SelectedItem as Tipo;

            if (tipoUsuarioAEliminar != null)
            {
                string mensaje = $"¿Está seguro que desea dar de baja el tipo usuairio {tipoUsuarioAEliminar.Nombre}?";

                Confirmacion ventanaConfirmacion = new Confirmacion
                {
                    Titulo = "Eliminar tipo usuario",
                    Mensaje = mensaje
                };

                if (ventanaConfirmacion.ShowDialog() == true)
                {
                    ResultadoRegistro resultado;
                    _tipoUsuarioController = new TipoUsuarioController();

                    try
                    {
                        resultado = _tipoUsuarioController.DarDeBajaTipoUsuario(tipoUsuarioAEliminar);
                    }
                    catch (Exception)
                    {
                        new Dialog
                        {
                            Titulo = "Error",
                            Mensaje = "Por el momento no pudimos realizar la operación. Intente más tarde."
                        }.ShowDialog();
                        return;
                    }

                    if (resultado == ResultadoRegistro.RegistroExitoso)
                    {
                        new Dialog
                        {
                            Titulo = "Operación exitosa",
                            Mensaje = "El tipo usuario fue dado de baja exitosamente."
                        }.ShowDialog();

                        CargarTiposUsuarios();
                    }
                    else if(resultado == ResultadoRegistro.RegistroFallido)
                    {
                        new Dialog
                        {
                            Titulo = "Error en la operación",
                            Mensaje = "Por el momento no pudimos realizar la operación. Intente más tarde."
                        }.ShowDialog();
                    }
                    else
                    {
                        new Dialog
                        {
                            Titulo = "Error en la operación",
                            Mensaje = "Ocurrió un error al intentar acceder a la información del tipo usuario. Intente más tarde."
                        }.ShowDialog();
                    }
                }
            }
        }

        private void AgregarNuevoTipo(object sender, RoutedEventArgs e)
        {
            ventanaRegistroTipoUsuario = new RegistroTipoUsuario();
            ventanaRegistroTipoUsuario.RegistroExitoso += new EventHandler(NuevoTipoRegistrado);

            ventanaRegistroTipoUsuario.ShowDialog();
        }

        private void NuevoTipoRegistrado(object sender, EventArgs e)
        {
            //_tiposUsuario.Add(ventanaRegistroTipoUsuario.TipoUsuarioRegistrado);
            CargarTiposUsuarios();
        }

        private void Regresar_Clic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListaDeUsuarios());
        }

        private void Buscar_Clic(object sender, RoutedEventArgs e)
        {

        }

        private void LimpiarBusqueda_Clic(object sender, RoutedEventArgs e)
        {

        }
    }
}
