using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Excepciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Presentacion.Ventanas;
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

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Page
    {
        private readonly List<string> estadosLista;
        public List<Tipo> TiposUsuario { get; set; }
        private bool _seActualizoUsername = false;
        private bool _seActualizoPassword = false;
        private readonly string _usernameEmpleado;
        private readonly Empleado _empleadoRespaldo;

        public EditarUsuario(Empleado empleado)
        {
            InitializeComponent();
            _usernameEmpleado = empleado.Username;
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
            TiposUsuario = tipoUsuarioController.ObtenerTiposUsuario();

            ListaTiposUsuario.ItemsSource = TiposUsuario;
            _empleadoRespaldo = new Empleado
            {
                Username = empleado.Username,
                TipoUsuario = empleado.TipoUsuario
            };
            DataContext = empleado;
            

            foreach (var tipoUsuario in ListaTiposUsuario.ItemsSource as List<Tipo>)
            {
                if (tipoUsuario.Nombre.Equals(empleado.TipoUsuario.Nombre))
                {
                    ListaTiposUsuario.SelectedItem = tipoUsuario;
                    break;
                }
            }
        }

        private void Cancelar_Clic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListaDeUsuarios());
        }

        private void Actualizar_Clic(object sender, RoutedEventArgs e)
        {
            Empleado empleadoAActualizar = DataContext as Empleado;


            if (!_empleadoRespaldo.Username.Equals(empleadoAActualizar.Username))
            {
                _seActualizoUsername = true;
            }

            if (!_empleadoRespaldo.TipoUsuario.Nombre.Equals(ListaTiposUsuario.Text))
                empleadoAActualizar.TipoUsuario = ListaTiposUsuario.SelectedItem as Tipo;

            if (!string.IsNullOrWhiteSpace(PasswordText.Password))
            {
                empleadoAActualizar.Contrasenia = PasswordText.Password;
                _seActualizoPassword = true;
            }

            Dialog ventanaDialog = new Dialog();

            if (EstaInformacionCorrecta(empleadoAActualizar))
            {
                EmpleadoController empleadoController = new EmpleadoController();
                ResultadoRegistro resultado;

                try
                {
                    resultado = empleadoController.ActualizarEmpleado(empleadoAActualizar, _seActualizoUsername, _seActualizoPassword);
                }
                catch (Exception)
                {
                    ventanaDialog.Titulo = "Error de actualización";
                    ventanaDialog.Mensaje = "Ocurrió un error al intentar actualizar el empleado. Intente más tarde.";
                    ventanaDialog.ShowDialog();
                    return;
                }

                switch (resultado)
                {
                    case ResultadoRegistro.RegistroExitoso:
                        ventanaDialog.Titulo = "Actualización exitosa";
                        ventanaDialog.Mensaje = "El empleado fue actualizado correctamente.";
                        ventanaDialog.ShowDialog();

                        NavigationService.Navigate(new ListaDeUsuarios());
                        break;
                    case ResultadoRegistro.RegistroFallido:
                        ventanaDialog.Titulo = "Error de actualización";
                        ventanaDialog.Mensaje = "Ocurrió un error al intentar actualizar el empleado. Intente más tarde.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.UsuarioYaExiste:
                        ventanaDialog.Titulo = "El usuario ya existe";
                        ventanaDialog.Mensaje = $"El usuario {empleadoAActualizar.Username} ya pertenece a otro empleado. Por favor ingrese uno diferente.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.DireccionNoEspecificada:
                        ventanaDialog.Titulo = "Error de actualización";
                        ventanaDialog.Mensaje = "La dirección es requerida, por favor especifiquela.";
                        ventanaDialog.ShowDialog();
                        break;
                    case ResultadoRegistro.InformacionIncorrecta:
                        ventanaDialog.Titulo = "Error de actualización";
                        ventanaDialog.Mensaje = "La información ingresada es incorrecta, por favor verifiquela.";
                        ventanaDialog.ShowDialog();
                        break;
                }
            }
            else
            {
                ventanaDialog.Titulo = "Información incorrecta";
                ventanaDialog.Mensaje = "La información ingresada en uno o varios campos es incorrecta. Por favor verifiquela e intente de nuevo.";
                ventanaDialog.ShowDialog();
            }
        }

        private bool EstaInformacionCorrecta(Empleado empleado)
        {
            ValidadorPersonas validadorPersona = new ValidadorPersonas();
            ValidadorDireccion validadorDireccion = new ValidadorDireccion();
            ValidadorEmpleado validadorEmpleado = new ValidadorEmpleado();

            return validadorDireccion.Validar(empleado.Direcciones[0]) && validadorPersona.Validar(empleado) &&
                validadorEmpleado.Validar(empleado);
        }
    }
}
