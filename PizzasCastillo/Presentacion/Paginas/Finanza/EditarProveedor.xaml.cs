using Dominio.Entidades;
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
    /// Lógica de interacción para EditarProveedor.xaml
    /// </summary>
    public partial class EditarProveedor : Page
    {
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private readonly List<string> estadosLista;
        private byte[] archivo;
        private const string MENSAJE_ARCHIVO = "Ya se tiene guardado un archivo si desea actualizarlo seleecione la opcion";
        private int idP;
        public EditarProveedor(Proveedor proveedorSeleccionado)
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
            this.DataContext = proveedorSeleccionado;
            ListaEstados.Text = proveedorSeleccionado.Direccion.EntidadFederativa;
            NombresText.Text = proveedorSeleccionado.Nombre;
            ApellidosText.Text = proveedorSeleccionado.NombreEncargado;
            NoInteriorText.Text = proveedorSeleccionado.Direccion.Numero;
            TelefonoText.Text = proveedorSeleccionado.Telefono;
            CalleText.Text = proveedorSeleccionado.Direccion.Calle;
            CiudadText.Text = proveedorSeleccionado.Direccion.Ciudad;
            CodigoPostalText.Text = proveedorSeleccionado.Direccion.CodigoPostal;
            DNIText.Text = proveedorSeleccionado.Dni;
            EmailText.Text = proveedorSeleccionado.Email;
            rutaArchivo.Text = MENSAJE_ARCHIVO;
            idP = proveedorSeleccionado.Id;
            archivo = proveedorSeleccionado.ListaDeProductos;

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

        private void ActualizarProveedorAccion(object sender, RoutedEventArgs e)
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
                Proveedor proveedor = new Proveedor
                {
                    Id = idP,
                    Nombre = NombresText.Text,
                    Dni = DNIText.Text,
                    Email = EmailText.Text,
                    Telefono = TelefonoText.Text,
                    NombreEncargado = ApellidosText.Text,
                    Direccion = direccion
                };

                if (rutaArchivo.Text != MENSAJE_ARCHIVO )
                {

                    byte[] archivoNuevo = null;
                    Stream stream = openFileDialog.OpenFile();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        archivo = ms.ToArray();
                    }

                    proveedor.ListaDeProductos = archivoNuevo;
                }
                else
                {
                    proveedor.ListaDeProductos = archivo;
                }


                if (ValidarCampos(proveedor, direccion))
                {
                    ProveedorController proveedorController = new ProveedorController();
                    bool guardado = proveedorController.ActualizarProovedor(proveedor);

                    if (guardado)
                    {
                        InteraccionUsuario ventana = new InteraccionUsuario("Exito en Actualizacion", "Se ha actualizado el proveedor y su direccion con exito");
                        ventana.Show();
                        //REGRESA PAGINA
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

    }
}
