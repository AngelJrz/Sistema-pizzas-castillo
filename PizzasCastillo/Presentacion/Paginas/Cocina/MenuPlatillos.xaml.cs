using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
