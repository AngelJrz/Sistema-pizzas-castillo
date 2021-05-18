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
                bool guardado = clienteController.GuardarCliente(persona);
                bool guardado1 = direccionController.GuardarDireccionCliente(direccion,persona.Telefono);
                if (guardado && guardado1)
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Exito en registro", "Se ha guardado el cliente y su direccion con exito");
                    ventana.Show();
                    //REGRESA PAGINA
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
    }
}
