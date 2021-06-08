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
    /// Lógica de interacción para Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        public Dialog()
        {
            InitializeComponent();
        }

        public Dialog(string titulo, string mensaje)
        {
            InitializeComponent();
            Titulo = titulo;
            Mensaje = mensaje;
        }
    }
}
