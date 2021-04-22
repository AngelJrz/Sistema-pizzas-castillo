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

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para TiposDeGasto.xaml
    /// </summary>
    public partial class TiposDeGasto : Page
    {
        public TiposDeGasto()
        {
            InitializeComponent();
        }

        private void RegistrarNuevoTipo(object sender, RoutedEventArgs e)
        {
            if (!nombreNuevoTipo.Text.Trim().Equals(""))
            {
                MessageBoxResult opscionSeleccionada = MessageBox.Show("Estas seguro de registrar el nuevo tipo?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (opscionSeleccionada == MessageBoxResult.Yes)
                {
                    MessageBox.Show("se registro nuevo tipo", "Exito");
                }
            }
            else
            {
                MessageBox.Show("El campo esta incompleto", "Error");
            }
        }

        private void ClickSalir(object sender, RoutedEventArgs e)
        {

        }

        private void ClickModificar(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Diste un click", "Felicidades", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
