﻿using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Recursos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly Thread verificadorDeBloqueoDeAcceso;
        private Process _teclado;
        private Singleton _sesion;

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
                MessageBox.Show("Ingrese todos los datos");
                return;
            }

            if (ExisteBloqueoDeAcceso())
            {
                MessageBox.Show("Ha superado los intentos de iniciar sesión. Por favor espero 5 minutos.");
                return;
            }

            if (AlcanzoLimiteDeIntentos())
            {
                MessageBox.Show("Ha alcanzado el límite intentos de iniciar sesión. Por favor espere 5 minutos.");
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
                MessageBox.Show("Ocurrió un error, por favor intente más tarde.");
                return;
            }

            if (empleado == null)
            {
                AumentarIntentosDeInicioDeSesion();
                MessageBox.Show("La información ingresada es incorrecta.");
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
