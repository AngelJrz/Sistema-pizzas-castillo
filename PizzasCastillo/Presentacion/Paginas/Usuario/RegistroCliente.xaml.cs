using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para RegistroCliente.xaml
    /// </summary>
    public partial class RegistroCliente : Page
    {
        private Process _teclado;
        private readonly List<string> estadosLista;
        public RegistroCliente()
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
            ListaEstados.ItemsSource = estadosLista;
        }
        private void RegistrarCliente(object sender, RoutedEventArgs e)
        {
            List<Direccion> direccions = new List<Direccion>();

            Direccion direccion = new Direccion
            {
                Calle = CalleText.Text,
                Colonia = ColoniaText.Text,
                Ciudad = CiudadText.Text,
                CodigoPostal = CodigoPostalText.Text,
                NumeroInterior = NoInteriorText.Text,
                Referencias = ReferenciasText.Text,
                NumeroExterior = NoExteriorText.Text,
                EntidadFederativa = ListaEstados.Text
            };

            direccions.Add(direccion);

            Persona persona = new Persona
            {
                Nombres = NombresText.Text,
                Apellidos = ApellidosText.Text,
                Telefono = TelefonoText.Text,
                Email = EmailText.Text,
                Estatus = 1,
                Direcciones = direccions
            };

            if (ValidarCampos(persona,direccion) == true)
            {
                ClienteController clienteController = new ClienteController();
                DireccionController direccionController = new DireccionController();
                ResultadoRegistro guardado = clienteController.RegistrarCliente(persona);

                switch (guardado)
                {
                    case ResultadoRegistro.UsuarioYaExiste:
                        InteraccionUsuario ventana = new InteraccionUsuario("Error de registro", "Este cliente ya se encuentra registrado");
                        ventana.Show();
                        break;
                    case ResultadoRegistro.RegistroFallido:
                        InteraccionUsuario ventana1 = new InteraccionUsuario("Error de registro", "Hubo un error a la hora del registro, Intente mas tarde");
                        ventana1.Show();
                        break;
                    case ResultadoRegistro.RegistroExitoso:
                        InteraccionUsuario ventana2 = new InteraccionUsuario("Exito en registro", "Se ha guardado el cliente y su direccion con exito");
                        ventana2.Show();
                        NavigationService.GoBack();
                        break;
                }
            }
            else
            {
                InteraccionUsuario ventana = new InteraccionUsuario("Error de campos","Uno o mas campos estan incorrectos,verificar bien");
                ventana.Show();
            }
        }

        public bool ValidarCampos(Persona persona, Direccion direccion) 
        {
            ValidadorPersonas validadorP = new ValidadorPersonas();
            ValidadorDireccion validadorD = new ValidadorDireccion();
            if (validadorD.Validar(direccion) == true && GenericValidatorText.IsEmail(EmailText.Text))
            {
                return validadorP.Validar(persona);
            }
            else
            {
                return false;
            }
        }

        private void ValidateNumber(object sender, TextChangedEventArgs e)
        {
            string number = ((TextBox)sender).Text;

            if (GenericValidatorText.IsANumber(number))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void IsTelephoneNumber(object sender, TextCompositionEventArgs e)
        {
            if (!GenericValidatorText.IsTelephoneNumber(e.Text))
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
    }

}
