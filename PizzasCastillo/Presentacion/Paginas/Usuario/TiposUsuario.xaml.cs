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
        private List<Tipo> tiposUsuariosConsultados;
        private RegistroTipoUsuario ventanaRegistroTipoUsuario;
        private EditarTipoUsuario ventanaEditar;

        public TiposUsuario()
        {
            InitializeComponent();

            _tipoUsuarioController = new TipoUsuarioController();
            tiposUsuariosConsultados = _tipoUsuarioController.ObtenerTiposUsuario();
            _tiposUsuario = new ObservableCollection<Tipo>(tiposUsuariosConsultados);
            ListaTiposUsuario.ItemsSource = _tiposUsuario;
        }

        private void BuscarEnter(object sender, KeyEventArgs e)
        {

        }

        private void ConsultarTipoUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EditarTipoUsuario(object sender, RoutedEventArgs e)
        {
            Tipo tipoUsuarioAEditar = ListaTiposUsuario.SelectedItem as Tipo;

            if (tipoUsuarioAEditar != null)
            {
                ventanaEditar = new EditarTipoUsuario(tipoUsuarioAEditar);
                ventanaEditar.ActualizacionExitosa += new EventHandler(TipoUsuarioEditado);

                ventanaEditar.ShowDialog();
            }
        }

        private void TipoUsuarioEditado(object sender, EventArgs e)
        {
            if(ventanaEditar.DialogResult == true)
            {
                Tipo tipoUsuarioActualizado = _tiposUsuario.FirstOrDefault(tipo =>
                    tipo.Id == ventanaEditar.TipoUsuario.Id || tipo.Nombre.Equals(ventanaEditar.TipoUsuario.Nombre));

                if (tipoUsuarioActualizado != null)
                {
                    tipoUsuarioActualizado.Id = ventanaEditar.TipoUsuario.Id;
                    tipoUsuarioActualizado.Nombre = ventanaEditar.TipoUsuario.Nombre;
                    tipoUsuarioActualizado.Estatus = ventanaEditar.TipoUsuario.Estatus;
                }
            }
        }

        private void EliminarTipoUsuario(object sender, RoutedEventArgs e)
        {
            Tipo tipoUsuarioAEliminar = ListaTiposUsuario.SelectedItem as Tipo;

            if (tipoUsuarioAEliminar != null)
            {
                string mensaje = $"¿Está seguro que desea eliminar el tipo usuairio {tipoUsuarioAEliminar.Nombre}?";

                Confirmacion ventanaConfirmacion = new Confirmacion
                {
                    Titulo = "Eliminar tipo usuario",
                    Mensaje = mensaje
                };

                if (ventanaConfirmacion.ShowDialog() == true)
                {
                    bool seDioDeBaja;

                    try
                    {
                        seDioDeBaja = _tipoUsuarioController.DarDeBajaTipoUsuario(tipoUsuarioAEliminar);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ocurrio un error. Intente más tarde");
                        return;
                    }

                    if (seDioDeBaja)
                    {
                        MessageBox.Show("Tipo usuario eliminado exitosamente.");
                        _tiposUsuario.Remove(tipoUsuarioAEliminar);

                        return;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el tipo usuario seleccionado.");
                        return;
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
            _tiposUsuario.Add(ventanaRegistroTipoUsuario.TipoUsuarioRegistrado);
        }
    }
}
