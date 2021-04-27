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
    /// Lógica de interacción para Confirmacion.xaml
    /// </summary>
    public partial class Confirmacion : Window
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        /// <summary>
        /// Crea una instancia de la ventana Confirmación.
        /// </summary>
        public Confirmacion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea una instancia de la ventana Confirmación para mostrar el título y el mensaje
        /// especificado.
        /// </summary>
        /// <param name="titulo">Título a mostrar en la ventana Confirmación.</param>
        /// <param name="mensaje">Mensaje a mostrar para la ventana Confirmación.</param>
        public Confirmacion(string titulo, string mensaje)
        {
            InitializeComponent();
            Titulo = titulo;
            Mensaje = mensaje;
        }

        private void AceptarClic(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
