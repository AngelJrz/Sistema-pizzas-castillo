using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Dominio.Logica;
using Dominio.Enumeraciones;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para TiposDeGasto.xaml
    /// </summary>
    public partial class TiposDeGasto : Page
    {
        private Tipo tipoSeleccionado;
        public TiposDeGasto()
        {
            InitializeComponent();
            ActualizarTablaDeTipos();
        }

        private void RegistrarNuevoTipo(object sender, RoutedEventArgs e)
        {
            if (!nombreNuevoTipo.Text.Trim().Equals(""))
            {
                MessageBoxResult opscionSeleccionada = MessageBox.Show("Estas seguro de registrar el nuevo tipo?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (opscionSeleccionada == MessageBoxResult.Yes)
                {
                    TipoDeGastoController controlador = new TipoDeGastoController();
                    Tipo nuevoTipo = new Tipo()
                    {
                        Nombre = nombreNuevoTipo.Text
                    };

                    if (controlador.GuardarNuevoTipoDeGasto(nuevoTipo))
                    {
                        MessageBox.Show("Se registro correctamente", "Exito");
                        ActualizarTablaDeTipos();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al registrar el nuevo tipo, verifica que no exista en la lista actual", "Error");
                    }  
                }
            }
            else
            {
                MessageBox.Show("El campo esta incompleto", "Error");
            }
        }

        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClickModificar(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Ingresa el nuevo nombre prueba");
            if (dialog.ShowDialog() == true)
            {
                if (!dialog.ResponseText.Trim().Equals(""))
                {
                    TipoDeGastoController controlador = new TipoDeGastoController();
                    tipoSeleccionado = (Tipo)tablaDeTipos.SelectedItem;

                    if (controlador.ModificarTiposDeGasto(tipoSeleccionado.Id, dialog.ResponseText))
                    {
                        MessageBox.Show("Actualizacion terminada");
                        ActualizarTablaDeTipos();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error, verifica que no exista en la lista actual");
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa la informacion solicitada");
                }
            }
        }

        private void ActualizarTablaDeTipos()
        {
            TipoDeGastoController gastoControlador = new TipoDeGastoController();
            List<Tipo> listaDeTipos = gastoControlador.ObtenerTiposDeGasto();

            if (listaDeTipos.Count == 0)
            {
                MessageBox.Show("No existen tipos de gasto actualmente", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                tablaDeTipos.ItemsSource = new ObservableCollection<Tipo>(listaDeTipos);
            }
        }
    }
}
