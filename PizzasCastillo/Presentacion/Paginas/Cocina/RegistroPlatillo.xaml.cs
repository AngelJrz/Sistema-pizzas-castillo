using Microsoft.Win32;
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

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para RegistroPlatillo.xaml
    /// </summary>
    public partial class RegistroPlatillo : Page
    {
        public RegistroPlatillo()
        {
            InitializeComponent();
        }

        private void agregarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                botonImagen.Content = "Cambiar foto";
            }

        }

        private void AgregarIngrediente(object sender, RoutedEventArgs e)
        {
            if (CamposVacios())
            {
                MessageBox.Show("Ingrese el nombre de la actividad y selecciona el mes correspondiente.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (YaExiste())
                {
                    MessageBox.Show("La actividad ya existe. Por favor verifique la información ingresada.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AgregarIngredienteLista();
                }
            }
            
        }

        private void AgregarIngredienteLista()
        {

        }


        private bool CamposVacios()
        {
            bool areEmpty = false;

            /*if (String.IsNullOrWhiteSpace(activityName.Text) || months.SelectedItem == null)
            {
                areEmpty = true;
            }
            */
            return areEmpty;
        }

        private bool YaExiste()
        {
            /*
            bool exist = false;

            Producto
            foreach (object activity in projectActivitiesListBox.Items)
            {
                ProjectActivity projectActivityAux = (activity as ProjectActivity);

                if (projectActivityAux.Name.Equals(activityName.Text) &&
                    projectActivityAux.Month.Equals(months.Text))
                {
                    exist = true;
                    break;
                }
            }

            return exist;
            */
            return true;
        }

    }
}
