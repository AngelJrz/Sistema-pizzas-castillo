using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Microsoft.Win32;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para RegistrarProveedor.xaml
    /// </summary>
    public partial class RegistrarProveedor : Page
    {

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private readonly List<string> estadosLista;
        public RegistrarProveedor()
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

        private void AbrirArchivo(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                rutaArchivo.Text = openFileDialog.FileName;
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
        public bool ValidarCampos(Proveedor proveedor, DireccionProveedor direccion)
        {
            ValidadorProveedor validadorP = new ValidadorProveedor();
            ValidadorDireccionProveedor validadorD = new ValidadorDireccionProveedor();
            if (validadorD.Validar(direccion) == true && GenericValidatorText.IsEmail(EmailText.Text))
            {
                return validadorP.Validar(proveedor);
            }
            else
            {
                return false;
            }
        }

        private void RegistrarProveedorAccion(object sender, RoutedEventArgs e)
        {
            try
            {
                DireccionProveedor direccion = new DireccionProveedor
                {
                    Calle = CalleText.Text,
                    Ciudad = CiudadText.Text,
                    CodigoPostal = CodigoPostalText.Text,
                    EntidadFederativa = ListaEstados.Text,
                    Numero = NoInteriorText.Text,
                };

                byte[] archivo = null;
                Stream stream = openFileDialog.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    archivo = ms.ToArray();
                }


                Proveedor proveedor = new Proveedor
                {
                    Nombre = NombresText.Text,
                    Dni = DNIText.Text,
                    Email = EmailText.Text,
                    Telefono = TelefonoText.Text,
                    NombreEncargado = ApellidosText.Text,
                    ListaDeProductos = archivo,
                    Direccion = direccion,
                    NombreArchivo = openFileDialog.SafeFileName
                };

                if (ValidarCampos(proveedor, direccion))
                {
                    ProveedorController proveedorController = new ProveedorController();
                    ResultadoRegistro guardado = proveedorController.GuardarProveedor(proveedor);

                    switch (guardado)
                    {
                        case ResultadoRegistro.UsuarioYaExiste:
                            InteraccionUsuario ventana = new InteraccionUsuario("Error de registro", "Este proveedor ya se encuentra registrado");
                            ventana.Show();
                            break;
                        case ResultadoRegistro.RegistroFallido:
                            InteraccionUsuario ventana1 = new InteraccionUsuario("Error de registro", "Hubo un error a la hora del registro, Intente mas tarde");
                            ventana1.Show();
                            break;
                        case ResultadoRegistro.RegistroExitoso:
                            InteraccionUsuario ventana2 = new InteraccionUsuario("Exito en registro", "Se ha guardado el proveedor y su direccion con exito");
                            ventana2.Show();
                            NavigationService.GoBack();
                            break;
                    }
                }
                else
                {

                    InteraccionUsuario ventana = new InteraccionUsuario("Error de campos", "Uno o mas campos estan incorrectos y/o vacios,verificar bien");
                    ventana.Show();
                }
            }
            catch (InvalidOperationException ex)
            {
                InteraccionUsuario ventana = new InteraccionUsuario("Error de campos", "No se ha agredado ningun archivo");
                ventana.Show();
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
