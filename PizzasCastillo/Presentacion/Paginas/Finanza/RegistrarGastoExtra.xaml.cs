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
using Dominio.Logica;
using Dominio.Enumeraciones;
using Dominio.Entidades;
using System.Collections.ObjectModel;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para RegistrarGastoExtra.xaml
    /// </summary>
    public partial class RegistrarGastoExtra : Page
    {
        private double sumaTotalDeGasto;
        private Empleado empledoRegistrando;

        public RegistrarGastoExtra()
        {
            InitializeComponent();
            TipoDeGastoController controlador = new TipoDeGastoController();
            List<Tipo> listaDeTipos = controlador.ObtenerTiposDeGasto();

            if (listaDeTipos.Count == 0)
            {
                MessageBox.Show("No existen tipos de gastos, intente mas tarde");
                NavigationService.GoBack();
            }
            else
            {
                tipoDeGasto.ItemsSource = listaDeTipos;
                tipoDeGasto.SelectedItem = listaDeTipos.ElementAt(0);
            }

            //Se va a quitar
            empledoRegistrando = new Empleado()
            {
                NumeroEmpleado = "1"
            };
        }

        private void ClickRegistrar(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                GastoExtraController controlador = new GastoExtraController();
                GastoExtra nuevoGasto = ObtenerGastoExtra();

                if (controlador.RegistrarGastoExtra(nuevoGasto))
                {
                    MessageBox.Show("Se registro el gasto exitosamente");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error durante el registro");
                }
            }
            else
            {
                MessageBox.Show("Los datos son invalidos");
            }
        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {

        }

        private bool ValidarCampos()
        {
            bool validos = false;

            if (!conceptoDeGasto.Text.Equals("") &&
                !total.Text.Equals(""))
            {
                try
                {
                    sumaTotalDeGasto = double.Parse(total.Text);
                    validos = true;
                }
                catch (Exception)
                {
                    return validos;
                }
            }
            return validos;
        }

        private GastoExtra ObtenerGastoExtra()
        {
            GastoExtra nuevoGasto = new GastoExtra()
            {
                RegistradoPor = empledoRegistrando,
                Comentario = conceptoDeGasto.Text,
                Total = new decimal(sumaTotalDeGasto),
                Fecha = DateTime.Now,
                Tipo = tipoDeGasto.SelectedItem as Tipo
            };

            return nuevoGasto;
        }
    }
}
