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
                new Dialog
                {
                    Titulo = "Información incompleta.",
                    Mensaje = "Por favor ingresa el nombre del nuevo tipo"
                }.ShowDialog();
                return;
            }

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();
            

            ResultadoRegistro resultado;
            Tipo nuevoTipoUsuario = new Tipo
            {
                Nombre = NombreText.Text
            };

            try
            {
                resultado = tipoUsuarioController.RegistrarNuevoTipoUsuario(nuevoTipoUsuario);
            }
            catch (Exception)
            {
                new Dialog
                {
                    Titulo = "Error",
                    Mensaje = "Por el momento no pudimos registrar el nuevo tipo. Intente más tarde."
                }.ShowDialog();
                return;
            }

            if (resultado == ResultadoRegistro.RegistroExitoso)
            {
                new Dialog
                {
                    Titulo = "Registro exitoso",
                    Mensaje = "Nuevo tipo registrado exitosamente."
                }.ShowDialog();

                NombreText.Text = "";
                if (RegistroExitoso != null)
                {
                    TipoUsuarioRegistrado = nuevoTipoUsuario;
                    RegistroExitoso(this, new EventArgs());
                }
                this.DialogResult = true;
            }
            else if (resultado == ResultadoRegistro.TipoUsuarioYaExiste)
            {
                new Dialog
                {
                    Titulo = "Tipo usuario existente",
                    Mensaje = "El tipo usuario ya se encuentra registrado. Por favor verifique la información."
                }.ShowDialog();
            }
        }
    }
}
