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
        private Tipo _tipoUsuario;
        public event EventHandler ActualizacionExitosa;
        private ResultadoRegistro _resultado;
        private bool _seActualizoNombre = false;

        public EditarTipoUsuario(Tipo tipoUsuario)
        {
            InitializeComponent();
            _tipoUsuario = tipoUsuario;
            DataContext = tipoUsuario;
        }

        private void ActualizarClic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NombreText.Text))
            {
                new Dialog
                {
                    Titulo = "Información faltante",
                    Mensaje = "Por favor ingrese el nombre del tipo usuario."
                }.ShowDialog();
                return;
            }

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();

            Tipo tipoUsuarioAActualizar = new Tipo
            {
                Id = _tipoUsuario.Id,
                Nombre = NombreText.Text,
                Estatus = ListaEstatus.Text == "Activo"? 1 : 0
            };


            try
            {
                _resultado = tipoUsuarioController.EditarTipoUsuario(tipoUsuarioAActualizar, _seActualizoNombre);
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

            if (_resultado == ResultadoRegistro.RegistroExitoso)
            {
                new Dialog
                {
                    Titulo = "Actualización exitosa",
                    Mensaje = "Tipo usuario actualizado exitosamente"
                }.ShowDialog();
                
                //if(ActualizacionExitosa != null)
                //{
                //    TipoUsuario = tipoUsuarioAActualizar;
                //    ActualizacionExitosa(this, new EventArgs());
                //}
                this.DialogResult = true;
            }
            else if(_resultado == ResultadoRegistro.TipoUsuarioYaExiste)
            {
                new Dialog
                {
                    Titulo = "Tipo usuario existente",
                    Mensaje = "El nombre del tipo usuario ya existe. Por favor verifique la información."
                }.ShowDialog();
            }
            else if (_resultado == ResultadoRegistro.RegistroFallido)
            {
                new Dialog
                {
                    Titulo = "Error con la información",
                    Mensaje = "Por el momento no pudimos no pudimos acceder a la información del tipo usuario. Intente más tarde."
                }.ShowDialog();
                
            }
        }

        private void NombreText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!NombreText.Text.Equals(_tipoUsuario.Nombre))
                _seActualizoNombre = true;
        }
    }
}
