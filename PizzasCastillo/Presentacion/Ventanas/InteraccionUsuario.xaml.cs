using System.Windows;

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