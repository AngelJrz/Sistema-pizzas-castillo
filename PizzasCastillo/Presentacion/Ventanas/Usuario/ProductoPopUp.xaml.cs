using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentacion.Ventanas.Usuario
{
    /// <summary>
    /// Lógica de interacción para ProductoPopUp.xaml
    /// </summary>
    public partial class ProductoPopUp : Window
    {
        public ArticuloVenta productoAgregado { get; set; }
        public event EventHandler RegistroExitoso;
        private Process _teclado;

        public ProductoPopUp(bool accion, List<ArticuloVenta> productosVentas,ArticuloVenta producto)
        {
            InitializeComponent();
            if (accion)
            {
                Titulo.Text = "Registro de producto";
                accionBoton.Content = "Registrar";
            }
            else
            {
                Titulo.Text = "Edicion de producto";
                accionBoton.Content = "Actualizar";
                productBox.SelectedItem = producto;
            }
            this.DataContext = productosVentas;
            productBox.ItemsSource = productosVentas;
        }

        private void RegistrarClic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(productBox.Text) || String.IsNullOrWhiteSpace(cantidadText.Text))
            {
                InteraccionUsuario error = new InteraccionUsuario("Error de campos","Porfavor llene los campos");
                error.Show();
                return;
            }
            else
            {
                productoAgregado = (ArticuloVenta)productBox.SelectedItem;
                try
                {
                    productoAgregado.CantidadLocal = Convert.ToDecimal(cantidadText.Text);
                    RegistroExitoso(this, new EventArgs());
                    return;
                }
                catch (System.FormatException ex)
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error de campos", "Porfavor Ingrese una cantidad numerica");
                    error.Show();

                }
            }
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
