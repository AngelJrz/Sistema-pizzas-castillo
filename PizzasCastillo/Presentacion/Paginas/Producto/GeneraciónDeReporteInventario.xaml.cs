using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Logica;
using Dominio.Utilerias;
using Microsoft.Win32;
using Presentacion.Recursos;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Presentacion.Paginas.Producto
{
    /// <summary>
    /// Interaction logic for GeneraciónDeReporteInventario.xaml
    /// </summary>
    public partial class GeneraciónDeReporteInventario : Page
    {
        private ObservableCollection<Dominio.Entidades.Reporta> productosReportados;
        private List<Dominio.Entidades.Producto> listaProductos;
        private List<Dominio.Entidades.Reporta> listaReportados;
        private Dominio.Entidades.Empleado empleadoActual;
        private ValidadorReporteInventario validadorReporteInventario;
        private Dominio.Entidades.Reporta productoSeleccionado;
        private readonly Singleton _sesion;

        public GeneraciónDeReporteInventario(Singleton sesion)
        {
            InitializeComponent();

            _sesion = sesion;

            ProductoController productoController = new ProductoController();
            listaProductos = productoController.ObtenerProductosActivos();
            listaReportados = new List<Dominio.Entidades.Reporta>();

            if (listaProductos.Count == 0)
            {
                MessageBox.Show("No existen Productos. Debe crear un Producto");

                NavigationService.GoBack();
            }  
            else
            {
                foreach (Dominio.Entidades.Producto p in listaProductos)
                {
                    Dominio.Entidades.Reporta item = new Dominio.Entidades.Reporta
                    {
                        Producto = p,
                        CantidadEnInventario = p.Cantidad
                    };

                    listaReportados.Add(item);
                }

                productosReportados = new ObservableCollection<Dominio.Entidades.Reporta>(listaReportados);
                TablaDeProductos.ItemsSource = productosReportados;
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GuardarReporte(object sender, RoutedEventArgs e)
        {
            Dominio.Entidades.ReporteInventario reporteNuevo = ObtenerReporte();



            if (EstaInformacionCorrecta(reporteNuevo))
            {
                ProductoController productoController = new ProductoController();
                Dominio.Entidades.Producto actualizacion = new Dominio.Entidades.Producto();

                foreach (Dominio.Entidades.Reporta reportado in listaReportados)
                {
                    actualizacion = reportado.Producto;
                    actualizacion.Cantidad = reportado.CantidadReal;
                    bool seActualizo = productoController.ActualizarCantidad(actualizacion);
                    if (!seActualizo)
                    {
                        MessageBox.Show("No se actualizo un producto del reporte");
                    }
                }

                ReporteInventarioController reporteController = new ReporteInventarioController();
                bool resultado;
                try
                {
                    resultado = reporteController.GuardarReporte(reporteNuevo);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrió un error al intentar guardar el reporte. Por favor intente más tarde.");
                    return;
                }

                if (resultado)
                {
                    MessageBox.Show("Se registró el reporte");
                    NavigationService.Navigate(new Inicio_Gerente_Productos(_sesion));
                }
                else
                {
                    MessageBox.Show("No se guardo el reporte. Intente mas tarde.");
                }
            }
            else
            {
                List<string> camposIncorecctos = validadorReporteInventario.ObtenerPropiedadesIncorrectas();
                string mensaje = "Los siguientes campos están incorrectos: ";
                foreach (var campos in camposIncorecctos)
                {
                    mensaje += campos + ", ";
                }

                mensaje += "por favor verifique la información.";

                MessageBox.Show(mensaje);
            }
        }

        private bool EstaInformacionCorrecta(Dominio.Entidades.ReporteInventario reporte)
        {
            ValidadorReporta validadorReporta = new ValidadorReporta();
            validadorReporteInventario = new ValidadorReporteInventario();

            bool estaCorrecto = false;
            
            foreach (Dominio.Entidades.Reporta reportado in reporte.Reporta)
            {
                estaCorrecto = validadorReporta.Validar(reportado);
                if (!estaCorrecto)
                {
                    break;
                }
            }
            return validadorReporteInventario.Validar(reporte) && estaCorrecto;
        }

        private void AgregarCantidad(object sender, RoutedEventArgs e)
        {
            InputDialog cantidad = new InputDialog("Ingresa la cantidad real del producto");

            if (cantidad.ShowDialog() == true)
            {
                if (EsNumeroValido(cantidad.ResponseText))
                {
                    Dominio.Entidades.Reporta productoSeleccionado = (Dominio.Entidades.Reporta)TablaDeProductos.SelectedItem;
                    productoSeleccionado.CantidadReal = Decimal.Parse(cantidad.ResponseText);
                    productosReportados = new ObservableCollection<Dominio.Entidades.Reporta>(listaReportados);
                    TablaDeProductos.ItemsSource = productosReportados;

                }
            }
        }

        private void AgregarComentario(object sender, RoutedEventArgs e)
        {
            InputDialog comentario = new InputDialog("Ingresa un comentario");

            if (comentario.ShowDialog() == true)
            {
                Dominio.Entidades.Reporta productoSeleccionado = (Dominio.Entidades.Reporta)TablaDeProductos.SelectedItem;
                productoSeleccionado.Comentario = comentario.ResponseText;
                productosReportados = new ObservableCollection<Dominio.Entidades.Reporta>(listaReportados);
                TablaDeProductos.ItemsSource = productosReportados;
            }
        }

        private void GenerarPDF(object sender, RoutedEventArgs e)
        {
            if (GuardarPDF())
            {
                MessageBox.Show("Se genero el PDF de reporte de inventario.");
            }
            else
            {
                MessageBox.Show("No se logro generar el PDF.");
            }
        }

        private bool GuardarPDF()
        {
            bool seGuardo = false;
            string rutaGuardado = ObtenerRutaDeGuardado();
            if(rutaGuardado != null)
            {
                ReporteInventarioController reporteController = new ReporteInventarioController();
                Dominio.Entidades.ReporteInventario reporte = ObtenerReporte();

                seGuardo = reporteController.GenerarPDF(reporte, rutaGuardado);
            }
            
            return seGuardo;
        }

        private Dominio.Entidades.ReporteInventario ObtenerReporte()
        {
            _sesion.Recursos.TryGetValue("Empleado", out object empleado);

            empleadoActual  = empleado as Empleado;

            Dominio.Entidades.ReporteInventario reporteActual = new Dominio.Entidades.ReporteInventario
            {
                Fecha = DateTime.Now,
                Nombre = NombreText.Text,
                GeneradoPor = empleadoActual,
                Reporta = listaReportados
            };

            return reporteActual;
        }

        private String ObtenerRutaDeGuardado()
        {
            SaveFileDialog saveWindow = new SaveFileDialog();
            saveWindow.Filter = "PDF Document|*.pdf";
            saveWindow.Title = "Selecciona ruta de guardado";
            saveWindow.ShowDialog();

            return saveWindow.FileName;
        }

        private bool EsNumeroValido(string texto)
        {
            try
            {
                double numero = double.Parse(texto.Trim());
                if (numero >= 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ingresa una cantidad valida");
                return false;
            }

            return false;
        }
    }
}
