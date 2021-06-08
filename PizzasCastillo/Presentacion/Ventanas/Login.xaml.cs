using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Recursos;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentacion.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly Thread verificadorDeBloqueoDeAcceso;
        private Process _teclado;
        private readonly Singleton _sesion;

        public Login()
        {
            InitializeComponent();
            _sesion = Singleton.ObtenerInstancia();
            
            verificadorDeBloqueoDeAcceso = new Thread(new ThreadStart(VerificarBloqueoDeAcceso));
            verificadorDeBloqueoDeAcceso.Start();
        }

        private void VerificarBloqueoDeAcceso()
        {
            while(true)
            {
                if ((ExisteBloqueoDeAcceso() == false) && (SobrepasoElLimiteDeIntentos() == true))
                {
                    DesbloquearAcceso();
                }
            }
        }

        private bool SobrepasoElLimiteDeIntentos()
        {
            int intentosDeInicioDeSesion = Properties.Settings.Default.IntentosDeInicioDeSesion;

            return intentosDeInicioDeSesion > 3;
        }

        private void DesbloquearAcceso()
        {
            Properties.Settings.Default.UltimoBloqueoDeAcceso.AddMinutes(-5);
            Properties.Settings.Default.IntentosDeInicioDeSesion = 0;
            Properties.Settings.Default.Save();
        }

        private void BloquearAcceso()
        {
            AumentarIntentosDeInicioDeSesion();
            Properties.Settings.Default.UltimoBloqueoDeAcceso = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        private void IniciarSesionBoton(object sender, RoutedEventArgs e)
        {
            IniciarSesion();
        }

        private void IniciarSesion()
        {
            if (EstanCamposVacios())
            {
                new Dialog
                {
                    Titulo = "Información incompleta",
                    Mensaje = "Por favor ingrese el usuario y contraseña."
                }.ShowDialog();
                return;
            }

            if (ExisteBloqueoDeAcceso())
            {
                new Dialog
                {
                    Titulo = "Bloqueo de acceso",
                    Mensaje = "Ha superado los intentos de iniciar sesión. Por favor espero 5 minutos."
                }.ShowDialog();
                return;
            }

            if (AlcanzoLimiteDeIntentos())
            {
                new Dialog
                {
                    Titulo = "Límite de intentos",
                    Mensaje = "Ha alcanzado el límite intentos de iniciar sesión. Por favor espere 5 minutos."
                }.ShowDialog();
                BloquearAcceso();
                return;
            }

            string username = usuarioText.Text;
            string password = passwordText.Password;

            EmpleadoController empleadoController = new EmpleadoController();

            Empleado empleado;
            try
            {
                empleado = empleadoController.IniciarSesion(username, password);
            }
            catch (Exception)
            {
                new Dialog
                {
                    Titulo = "Error",
                    Mensaje = "Ocurrió un error, por favor intente más tarde."
                }.ShowDialog();
                return;
            }

            if (empleado == null)
            {
                AumentarIntentosDeInicioDeSesion();
                new Dialog
                {
                    Titulo = "Información incorrecta",
                    Mensaje = "El usuario y/o contraseña son incorrectos. Por favor verifiquelos e intente de nuevo."
                }.ShowDialog();
                return;
            }

            ReiniciarIntentosDeInicioSesion();
            _sesion.Recursos.Add("Empleado", empleado);

            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal(_sesion);
            ventanaPrincipal.Show();
            this.Close();

            verificadorDeBloqueoDeAcceso.Join();
            
        }

        private void ReiniciarIntentosDeInicioSesion()
        {
            Properties.Settings.Default.IntentosDeInicioDeSesion = 0;
            Properties.Settings.Default.Save();
        }

        private bool AlcanzoLimiteDeIntentos()
        {
            int intentosDeInicioDeSesion = Properties.Settings.Default.IntentosDeInicioDeSesion;

            return intentosDeInicioDeSesion == 3;
        }

        private void AumentarIntentosDeInicioDeSesion()
        {
            Properties.Settings.Default.IntentosDeInicioDeSesion ++;
            Properties.Settings.Default.Save();
        }

        private bool ExisteBloqueoDeAcceso()
        {
            
            DateTime ultimaHoraDeBloqueo = Properties.Settings.Default.UltimoBloqueoDeAcceso;
            if (ultimaHoraDeBloqueo == null)
                return false;

            DateTime horaActual = DateTime.Now;
            return ultimaHoraDeBloqueo.AddMinutes(5) > horaActual;
        }

        private bool EstanCamposVacios()
        {
            return String.IsNullOrWhiteSpace(usuarioText.Text) || 
                String.IsNullOrWhiteSpace(passwordText.Password);
        }

        private void IniciarSesionTouch(object sender, TouchEventArgs e)
        {
            IniciarSesion();
        }

        private void ControlarTeclaEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IniciarSesion();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ExisteBloqueoDeAcceso() == false)
                ReiniciarIntentosDeInicioSesion();

            verificadorDeBloqueoDeAcceso.Abort();
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
            if(_teclado != null)
                _teclado.Kill();
        }
    }
}
