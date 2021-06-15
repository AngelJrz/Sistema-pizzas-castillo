using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para MenuMerma.xaml
    /// </summary>
    public partial class MenuMerma : Page
    {
        public MenuMerma()
        {
            InitializeComponent();
        }

        private void MermaPedido(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GeneracionMermaPedido());
        }

        private void MermaInsumo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GeneracionMermaIngredientes());
        }
    }
}
