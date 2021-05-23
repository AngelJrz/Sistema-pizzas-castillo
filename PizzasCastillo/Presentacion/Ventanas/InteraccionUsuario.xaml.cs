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

namespace Presentacion.Ventanas
{
    /// <summary>
    /// Lógica de interacción para InteraccionUsuario.xaml
    /// </summary>
    public partial class InteraccionUsuario : Window
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public InteraccionUsuario(string titulo, string mensaje)
        {
            InitializeComponent();
            Titulo = titulo;
            Mensaje = mensaje;
        }

        private void AceptarClic(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}