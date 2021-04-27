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
using Presentacion.Ventanas.Usuario;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para TiposUsuario.xaml
    /// </summary>
    public partial class TiposUsuario : Page
    {
        public TiposUsuario()
        {
            InitializeComponent();

            ListaTiposUsuario.ItemsSource = new List<TipoUsuario>()
            {
                new TipoUsuario{ Nombre = "Encargado de caja", Estatus = "Activo"},
                new TipoUsuario { Nombre = "Mesero", Estatus = "Activo" }
            };
        }

        private void BuscarEnter(object sender, KeyEventArgs e)
        {

        }

        private void ConsultarTipoUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EditarTipoUsuario(object sender, RoutedEventArgs e)
        {
            new EditarTipoUsuario
            {

            }.ShowDialog();
        }

        private void EliminarTipoUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void AgregarNuevoTipo(object sender, RoutedEventArgs e)
        {
            new RegistroTipoUsuario
            {

            }.ShowDialog();
        }
    }

    public class TipoUsuario
    {
        public string Nombre { get; set; }
        public string Estatus { get; set; }
    }
}
