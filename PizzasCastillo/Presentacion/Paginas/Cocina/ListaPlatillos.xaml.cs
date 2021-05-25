using Dominio.Entidades;
using Dominio.Logica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
    /// Lógica de interacción para ListaPlatillos.xaml
    /// </summary>
    public partial class ListaPlatillos : Page
    {
        ObservableCollection<ArticuloVenta> productos;
        private List<ArticuloVenta> platillosList;

        public ListaPlatillos()
        {
            InitializeComponent(); ArticuloVentaController articuloController = new ArticuloVentaController();
            platillosList = articuloController.ObtenerPlatillos();
            platilloList.ItemsSource = platillosList;

            foreach (ArticuloVenta p in platillosList)
            {
                try
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + p.NombreFoto, p.Foto);

                }
                catch(System.IO.IOException)
                {

                }
            }
        }

        private void EditarPlatillo(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)platilloList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            NavigationService.Navigate(new EditarPlatillo(platilloList.SelectedItem as ArticuloVenta));
        }
    }
}
