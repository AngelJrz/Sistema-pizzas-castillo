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
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Excepciones;
using Dominio.Logica;
using Dominio.Utilerias;

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

        private void Registrar_Clic(object sender, RoutedEventArgs e)
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

            if (EstaInformacionCorrecta(empleado))
            {
                EmpleadoController empleadoController = new EmpleadoController();
                bool seRegistro;
                try
                {
                    seRegistro = empleadoController.RegistrarEmpleado(empleado);
                }
                catch(UsuarioYaExisteException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrió un error al intentar registrar el usuario. Por favor intente más tarde.");
                    return;
                }
                

                if (seRegistro)
                {
                    MessageBox.Show("Se registró el empleado");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error, intenté más tarde");
                }
            }
            else
            {
                string mensaje = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
          

                MessageBox.Show(mensaje);
            }
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

    }
}
