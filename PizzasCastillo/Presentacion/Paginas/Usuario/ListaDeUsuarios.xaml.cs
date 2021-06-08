using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Presentacion.Recursos;
using Presentacion.Ventanas;

namespace Presentacion.Paginas.Usuario
{
    /// <summary>
    /// Lógica de interacción para ListaDeUsuarios.xaml
    /// </summary>
    public partial class ListaDeUsuarios : Page
    {
        private ObservableCollection<Empleado> _empleados;
        private EmpleadoController _empleadoController;
        private List<Empleado> busquedaEmpleados;

        public ListaDeUsuarios()
        {
            InitializeComponent();

            _empleadoController = new EmpleadoController();
            busquedaEmpleados = _empleadoController.ObtenerEmpleadosActivos();
            _empleados = new ObservableCollection<Empleado>(busquedaEmpleados);
            ListaUsuarios.ItemsSource = _empleados;
        }

        private void ConsultarUsuario(object sender, RoutedEventArgs e)
        {

        }

        private void EditarUsuario(object sender, RoutedEventArgs e)
        {
            Empleado empleadoSeleccionado = ListaUsuarios.SelectedItem as Empleado;

            NavigationService.Navigate(new EditarUsuario(empleadoSeleccionado) {  });
        }

        private void EliminarUsuario(object sender, RoutedEventArgs e)
        {
            
            Empleado empleadoSeleccionado = (Empleado)ListaUsuarios.SelectedItem;
            
            Singleton.ObtenerInstancia().Recursos.TryGetValue("Empleado", out object empleadoEnSesion);

            if (empleadoSeleccionado.NumeroEmpleado.Equals((empleadoEnSesion as Empleado).NumeroEmpleado))
            {
                new Dialog
                {
                    Titulo = "Error",
                    Mensaje = "El usuario que intenta dar de baja es el que está en sesión ahora mismo. Está operación no puede ser realizada."
                }.ShowDialog();
                return;
            }

            Confirmacion dialogoConfirmacion = new Confirmacion("Dar de baja usuario",
                $"¿Seguro desea dar de baja al usuario con No. Empleado {empleadoSeleccionado.NumeroEmpleado}?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                bool seDioDeBaja;
                EmpleadoController empleadoController = new EmpleadoController();

                try
                {
                    seDioDeBaja = empleadoController.DarDeBajaEmpleado(empleadoSeleccionado.NumeroEmpleado);
                }
                catch (Exception)
                {
                    new Dialog
                    {
                        Titulo = "Error",
                        Mensaje = "Ocurrio un error. Intente más tarde"
                    }.ShowDialog();
                    return;
                }

                if (seDioDeBaja)
                {
                    new Dialog
                    {
                        Titulo = "Empleado dado de baja",
                        Mensaje = "El empleado fue dado de baja exitosamente."
                    }.ShowDialog();
                    _empleados.Remove(empleadoSeleccionado);
                }
                else
                {
                    new Dialog
                    {
                        Titulo = "Error",
                        Mensaje = "No se pudo eliminar el tipo usuario seleccionado."
                    }.ShowDialog();
                }
            }
        }

        private void BuscarEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RealizarBusqueda();
            }
        }

        private void RealizarBusqueda()
        {
            string busqueda = BusquedaText.Text;

            if(String.IsNullOrWhiteSpace(busqueda))
            {
                new Dialog
                {
                    Titulo = "Información incompleta",
                    Mensaje = "Por favor ingresa información en el campo de búsqueda."
                }.ShowDialog();
                return;
            }

            string tipoBusqueda = TipoBusqueda.Text;

            switch (tipoBusqueda)
            {
                case "Nombre":
                    busquedaEmpleados = _empleadoController.ObtenerEmpleadosPorNombre(busqueda);
                    break;
                case "Teléfono":
                    busquedaEmpleados = _empleadoController.ObtenerEmpleadosPorTelefono(busqueda);
                    break;
                case "No. Empleado":
                    busquedaEmpleados = _empleadoController.ObtenerEmpleadosPorNumeroEmpleado(busqueda);
                    break;
            }

            _empleados = new ObservableCollection<Empleado>(busquedaEmpleados);
            ListaUsuarios.ItemsSource = _empleados;
            LimpiarBusquedaBtn.Visibility = Visibility.Visible;
        }

        private void IrARegistrarUsuario(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistroDeUsuario());
        }

        private void Buscar_Clic(object sender, RoutedEventArgs e)
        {
            RealizarBusqueda();
        }

        private void LimpiarBusqueda_Clic(object sender, RoutedEventArgs e)
        {
            busquedaEmpleados = _empleadoController.ObtenerEmpleadosActivos();
            _empleados = new ObservableCollection<Empleado>(busquedaEmpleados);
            ListaUsuarios.ItemsSource = _empleados;
            BusquedaText.Text = "";
            LimpiarBusquedaBtn.Visibility = Visibility.Collapsed;
        }

        private void IrATiposUsuarios(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TiposUsuario());
        }
    }
}