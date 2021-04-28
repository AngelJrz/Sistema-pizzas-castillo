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
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para ListaDeUsuarios.xaml
    /// </summary>
    public partial class ListaDeUsuarios : Page
    {
        public ListaDeUsuarios()
        {
            InitializeComponent();
            List<Empleado> empleados = new List<Empleado>()
            {
                new Empleado
                {
                    Nombre = "Roberto Perez Perez",
                    NumeroEmpledado = "GER0001",
                    Telefono = "2281122343",
                    TipoUsuario = "Gerente",
                    Estatus = "Activo"
                },

                new Empleado
                {
                    Nombre = "Bertha Carolina Hermida Altamirano",
                    NumeroEmpledado = "CAJ0001",
                    Telefono = "2281122344",
                    TipoUsuario = "Encargado de caja",
                    Estatus = "Activo"
                }
            };

            ListaUsuarios.ItemsSource = empleados;
        }

        private void ConsultarUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EditarUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EliminarUsuario(object sender, RoutedEventArgs e)
        {
            Empleado empleadoSeleccionado = (Empleado)ListaUsuarios.SelectedItem;
            Confirmacion dialogoConfirmacion = new Confirmacion("Dar de baja usuario",
                $"¿Seguro desea dar de baja al usuario con No. Empleado {empleadoSeleccionado.NumeroEmpledado} seleccionado?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {

            }
        }

        private void BuscarEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }

        private void IrARegistrarUsuario(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistroDeUsuario());
        }
    }

    public class Empleado
    {
        public string NumeroEmpledado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string TipoUsuario { get; set; }
        public string Estatus { get; set; }
    }
}
