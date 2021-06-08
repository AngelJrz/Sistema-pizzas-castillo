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
    /// Lógica de interacción para MenuPlatillos.xaml
    /// </summary>
    public partial class MenuPlatillos : Page
    {
        public MenuPlatillos()
        {
            InitializeComponent();
        }

        private void RevisarPlatillos(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListaPlatillos());
        }

        private void RegistrarPlatillo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistroPlatillo());

        }
    }
}
