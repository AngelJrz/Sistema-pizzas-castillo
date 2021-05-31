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
