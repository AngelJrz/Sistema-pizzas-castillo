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
    /// Lógica de interacción para RegistroTipoUsuario.xaml
    /// </summary>
    public partial class RegistroTipoUsuario : Window
    {
        public Tipo TipoUsuarioRegistrado { get; set; }
        public event EventHandler RegistroExitoso;
        public RegistroTipoUsuario()
        {
            InitializeComponent();
        }

        private void RegistrarClic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NombreText.Text))
            {
                MessageBox.Show("Por favor ingresa el nombre del nuevo tipo");
                return;
            }

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();
            

            bool seRegistro;
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = NombreText.Text
            };

            try
            {
                seRegistro = tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al registrar el nuevo tipo. Intente más tarde");
                return;
            }

            if (seRegistro)
            {
                MessageBox.Show("Nuevo tipo registrado exitosamente");
                NombreText.Text = "";
                if (RegistroExitoso != null)
                {
                    TipoUsuarioRegistrado = nuevoTipoUsuario;
                    RegistroExitoso(this, new EventArgs());
                }
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
