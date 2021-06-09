using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Excepciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para RegistroDeUsuario.xaml
    /// </summary>
    public partial class RegistroDeUsuario : Page
    {
        private readonly List<string> estadosLista;
        private readonly List<Tipo> tiposUsuario;
        private ValidadorDireccion validadorDireccion;
        private Process _teclado;

        public RegistroDeUsuario()
        {
            InitializeComponent();

            estadosLista = new List<string>
            {
                "Aguascalientes",
                "Baja California",
                "Baja California Sur",
                "Campeche",
                "Chiapas",
                "Chihuahua",
                "Coahuila",
                "Colima",
                "Ciudad de México / Distrito Federal",
                "Durango",
                "Estado de México",
                "Guanajuato",
                "Guerrero",
                "Hidalgo",
                "Jalisco",
                "Michoacán",
                "Morelos",
                "Nayarit",
                "Nuevo León",
                "Oaxaca",
                "Puebla",
                "Querétaro",
                "Quintana Roo",
                "San Luis Potosí",
                "Sinaloa",
                "Sonora",
                "Tabasco",
                "Tamaulipas",
                "Tlaxcala",
                "Veracruz",
                "Yucatán",
                "Zacatecas"
            };

            ListaEstidadesFederativas.ItemsSource = estadosLista;

            TipoUsuarioController tipoUsuarioController = new TipoUsuarioController();
            tiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            ListaTiposUsuario.ItemsSource = tiposUsuario;
        }

        private void RegistrarUsuario()
        {
            Direccion direccion = new Direccion
            {
                Calle = CalleText.Text,
                Colonia = ColoniaText.Text,
                Ciudad = CiudadText.Text,
                CodigoPostal = CodigoPostalText.Text,
                NumeroInterior = NoInteriorText.Text,
                Referencias = ReferenciasText.Text,
                NumeroExterior = NoExteriorText.Text,
                EntidadFederativa = ListaEstidadesFederativas.Text
            };

            decimal.TryParse(SalarioText.Text, out decimal salario);
            Empleado empleado = new Empleado
            {
                Nombres = NombresText.Text,
                Apellidos = ApellidosText.Text,
                Telefono = TelefonoText.Text,
                Email = EmailText.Text,
                TipoUsuario = ListaTiposUsuario.SelectedItem as Tipo,
                Username = UsernameText.Text,
                Contrasenia = PasswordText.Password,
                SalarioQuincenal = salario
            };
            empleado.Direcciones.Add(direccion);

            Dialog ventanaDialog = new Dialog();

            if (EstaInformacionCorrecta(empleado))
            {
                EmpleadoController empleadoController = new EmpleadoController();
                ResultadoRegistro resultado;
                try
                {
                    resultado = empleadoController.RegistrarEmpleado(empleado);
                }
                catch (Exception)
                {
                    ventanaDialog.Titulo = "Error de registro";
                    ventanaDialog.Mensaje = "Ocurrió un error al intentar registrar el usuario. Por favor intente más tarde.";

                    ventanaDialog.ShowDialog();
                    return;
                }

                switch (resultado)
                {
                    case ResultadoRegistro.RegistroExitoso:
                        ventanaDialog.Titulo = "Registro exitoso";
                        ventanaDialog.Mensaje = "El empleado fue registrado correctamente.";
                        ventanaDialog.ShowDialog();

                        LimpiarCampos();
                        break;
                    case ResultadoRegistro.RegistroFallido:
                        ventanaDialog.Titulo = "Error de registro";
                        ventanaDialog.Mensaje = "Ocurrió un error al intentar registrar el empleado. Intente más tarde.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.UsuarioYaExiste:
                        ventanaDialog.Titulo = "El usuario ya existe";
                        ventanaDialog.Mensaje = $"El usuario {empleado.Username} ya pertenece a otro empleado. Por favor ingrese uno diferente.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.DireccionNoEspecificada:
                        ventanaDialog.Titulo = "Error de registro";
                        ventanaDialog.Mensaje = "La dirección es requerida, por favor especifiquela.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.InformacionIncorrecta:
                        ventanaDialog.Titulo = "Error de actualización";
                        ventanaDialog.Mensaje = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
                        ventanaDialog.ShowDialog();
                        break;
                }
            }
            else
            {
                ventanaDialog.Titulo = "Error de registro";
                ventanaDialog.Mensaje = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
                ventanaDialog.ShowDialog();
            }
        }

        private void Registrar_Clic(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario();
        }

        private void LimpiarCampos()
        {
            ListaTiposUsuario.SelectedItem = null;
            UsernameText.Text = "";
            PasswordText.Password = "";
            SalarioText.Text = "";

            CalleText.Text = "";
            ColoniaText.Text = "";
            NoExteriorText.Text = "";
            NoInteriorText.Text = "";
            CodigoPostalText.Text = "";
            ListaEstidadesFederativas.SelectedItem = null;
            CiudadText.Text = "";
            ReferenciasText.Text = "";

            NombresText.Text = "";
            ApellidosText.Text = "";
            EmailText.Text = "";
            TelefonoText.Text = "";
        }

        private bool EstaInformacionCorrecta(Empleado empleado)
        {
            ValidadorPersonas validadorPersona = new ValidadorPersonas();
            validadorDireccion = new ValidadorDireccion();
            ValidadorEmpleado validadorEmpleado = new ValidadorEmpleado();

            return validadorDireccion.Validar(empleado.Direcciones[0]) && validadorPersona.Validar(empleado) &&
                validadorEmpleado.Validar(empleado);
        }

        private void Cancelar_Clic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListaDeUsuarios());
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

        private void CerrarTeclado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!_teclado.HasExited)
            {
                if (_teclado != null)
                    _teclado.Kill();
            }
        }

        private void Cancelar_Touch(object sender, TouchEventArgs e)
        {
            NavigationService.Navigate(new ListaDeUsuarios());
        }

        private void Registrar_Touch(object sender, TouchEventArgs e)
        {
            RegistrarUsuario();
        }
    }
}
