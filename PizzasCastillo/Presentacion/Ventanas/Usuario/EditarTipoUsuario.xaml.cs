using Dominio.Enumeraciones;
using Dominio.Logica;
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
using System.Windows.Shapes;

namespace Presentacion.Ventanas.Usuario
{
    /// <summary>
    /// Lógica de interacción para EditarTipoUsuario.xaml
    /// </summary>
    public partial class EditarTipoUsuario : Window
    {
        public Tipo TipoUsuario { get; set; }
        public event EventHandler ActualizacionExitosa;
        public bool SeActualizo { get; set; }
        public EditarTipoUsuario(Tipo tipoUsuario)
        {
            InitializeComponent();
            TipoUsuario = tipoUsuario;
        }

        private void ActualizarClic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NombreText.Text))
            {
                MessageBox.Show("Por favor ingresa el nombre del nuevo tipo");
                return;
            }

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            Tipo tipoUsuarioAActualizar = new Tipo
            {
                Id = TipoUsuario.Id,
                Nombre = NombreText.Text,
                Estatus = ListaEstatus.Text == "Activo"? 1 : 0
            };


            try
            {
                SeActualizo = tipoUsuarioController.RegistrarNuevoTipoUsuario(tipoUsuarioAActualizar);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al registrar el nuevo tipo. Intente más tarde");
                return;
            }

            if (SeActualizo)
            {
                MessageBox.Show("Tipo usuario actualizado exitosamente");
                NombreText.Text = "";
                TipoUsuario = tipoUsuarioAActualizar;
                ActualizacionExitosa?.Invoke(this, new EventArgs());
                this.DialogResult = true;
                return;
            }
            else
            {
                MessageBox.Show("No se pudo registrar el nuevo tipo. Intente más tarde");
                return;
            }
        }
    }
}
