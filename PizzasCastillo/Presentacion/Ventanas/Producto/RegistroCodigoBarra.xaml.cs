using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentacion.Ventanas.Producto
{
    /// <summary>
    /// Interaction logic for RegistroCodigoBarra.xaml
    /// </summary>
    public partial class RegistroCodigoBarra : Window
    {
        private Process _teclado;
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

        private void AbrirTeclado_Touch(object sender, TouchEventArgs e)
        {
            _teclado = Process.Start("osk.exe");

            if (sender.GetType() == typeof(TextBox))
            {
                ((TextBox)sender).Focus();
            }
            else
            {
                ((PasswordBox)sender).Focus();
            }
        }

        private void CerrarTeclado(object sender, RoutedEventArgs e)
        {
            if (_teclado != null)
            {
                if (!_teclado.HasExited)
                    _teclado.Kill();
            }
        }
    }
}
