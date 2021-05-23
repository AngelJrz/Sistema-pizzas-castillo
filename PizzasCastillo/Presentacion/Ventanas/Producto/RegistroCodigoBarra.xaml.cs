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

namespace Presentacion.Ventanas.Producto
{
    /// <summary>
    /// Interaction logic for RegistroCodigoBarra.xaml
    /// </summary>
    public partial class RegistroCodigoBarra : Window
    {
        public string codigoIngresado { get; set; }
        public RegistroCodigoBarra()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (String.IsNullOrWhiteSpace(CodigoText.Text))
                {
                    MessageBox.Show("Por favor ingresa un código de barra");
                    return;
                }

                codigoIngresado = CodigoText.Text;
                codigoIngresado.Trim();

                if (codigoIngresado.Length < 10)
                {
                    MessageBox.Show("El código de barra debe tener un tamaño de 10 caracteres");
                    return;
                }

                MessageBox.Show("Código capturado exitosamente");
                CodigoText.Text = "";
                this.DialogResult = true;
                return;
            }
        }
        private void RegistrarClic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CodigoText.Text))
            {
                MessageBox.Show("Por favor ingresa un código de barra");
                return;
            }

            codigoIngresado = CodigoText.Text;
            codigoIngresado.Trim();

            if (codigoIngresado.Length != 10)
            {
                MessageBox.Show("El código de barra debe tener un tamaño de 10 caracteres");
                return;
            }

            MessageBox.Show("Código capturado exitosamente");
            CodigoText.Text = "";
            this.DialogResult = true;
            return;
        }
    }
}
