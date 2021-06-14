using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dominio.Logica;
using Presentacion.Ventanas;
using static Presentacion.Recursos.PedidosResults;

namespace Presentacion.Paginas.Pedido
{
    /// <summary>
    /// Interaction logic for RegistroDePagoDePedido.xaml
    /// </summary>
    public partial class RegistroDePagoDePedido : Page
    {
        private Process _teclado;
        private Dominio.Entidades.Pedido _pedido;
        private decimal entregaCalculada;

        public RegistroDePagoDePedido(Dominio.Entidades.Pedido pedido)
        {
            InitializeComponent();

            _pedido = pedido;
            EmpleadoController empleadoController = new EmpleadoController();
            List<Dominio.Entidades.Empleado> empleadoList = empleadoController.ObtenerEmpleadosActivos();
            Dominio.Entidades.Empleado empleado = empleadoList.Find(e => e.NumeroEmpleado.Equals(pedido.RegistradoPor.NumeroEmpleado));
            _pedido.RegistradoPor = empleado;
            this.DataContext = _pedido;
        }

        private void ConfirmarAccion(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Confirmar Actualización", "¿Estas seguro que deseas actualizar este pedido a Pagado?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                RegistrarPago();
            }
        }

        private void RegistrarPago()
        {
            string mensaje = "";

            if (!_pedido.Estatus.Nombre.Equals("Entregado"))
            {
                mensaje = "Debe ser Estatus tipo Entregado el pedido para cambiarlo a Registrado";
            }

            if (entregaCalculada < 0)
            {
                mensaje = "Debe ser mayor o igual al total del Pedido la Cantidad a Recibir";
            }

            if (String.IsNullOrEmpty(entregaText.Text))
            {
                mensaje = "Debe ingresar una Cantidad a Recibir";
            }

            PedidoController pedidoController = new PedidoController();
            bool resultado;
            string pago = pagoText.Text;
            resultado = pedidoController.ActualizarAPagado(_pedido, Convert.ToDecimal(pago));

            if (resultado)
            {
                //InteraccionUsuario exito = new InteraccionUsuario("Exito", "Se actualizó a Pagado el Pedido. El cambio a retornar es: " + entregaCalculada);
                InteraccionUsuario exito = new InteraccionUsuario("Exito", "Se actualizó a Pagado el Pedido.");
                exito.Show();
                NavigationService.GoBack();
            }
            else
            {
                InteraccionUsuario error = new InteraccionUsuario("Error de Actualizar", mensaje);
                error.Show();
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void IngresarPago(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string pago = pagoText.Text;
            try
            {
                entregaCalculada = Convert.ToDecimal(pago) - _pedido.Total;
                entregaText.Text = entregaCalculada.ToString();
            }
            catch(Exception)
            {
                entregaText.Text = "";
            }
        }

        private void PermitirNumero(object sender, TextCompositionEventArgs e)
        {
            if (!EsNumero(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
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

        public static bool EsNumero(string number)
        {
            Regex numberRegularExpression = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

            return numberRegularExpression.IsMatch(number);
        }
    }
}
