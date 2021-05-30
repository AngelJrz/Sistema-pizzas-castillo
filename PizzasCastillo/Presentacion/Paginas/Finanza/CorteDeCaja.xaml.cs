using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Presentacion.Ventanas;
using Dominio.Enumeraciones;
using Dominio.Entidades;
using Dominio.Logica;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para CorteDeCaja.xaml
    /// </summary>
    public partial class CorteDeCaja : Page
    {
        private double efectivoDiaSiguiente;
        private List<TextBox> listaDeCampos;
        private List<Efectivo> listaDeTiposEfectivo;
        private Empleado empleado;
        private GastoExtraController controladorGastos;
        private PedidoController controladorPedidos;

        public CorteDeCaja(Empleado empleadoEnSesion)
        {
            empleado = empleadoEnSesion;

            InitializeComponent();

            ColocarIngresosYEgresos();

            listaDeCampos = new List<TextBox>();
            listaDeCampos.Add(campoBilleteMil);
            listaDeCampos.Add(campoBilleteQuinientos);
            listaDeCampos.Add(campoBilleteDoscientos);
            listaDeCampos.Add(campoBilleteCien);
            listaDeCampos.Add(campoBilleteCincuenta);
            listaDeCampos.Add(campoBilleteVeinte);

            listaDeCampos.Add(campoMonedaDiez);
            listaDeCampos.Add(campoMonedaCinco);
            listaDeCampos.Add(campoMonedaDos);
            listaDeCampos.Add(campoMonedaUno);
            listaDeCampos.Add(campoMonedaCentavo);

            EfectivoController efectivocontroller = new EfectivoController();
            listaDeTiposEfectivo = efectivocontroller.ObtenerTiposDeEfectivo();
        }

        public void ColocarIngresosYEgresos()
        {
            double totalGastos = controladorGastos.ObtenerSumaDeGastosDelDia();
            double totalIngresos = controladorPedidos.ObtenerIngresosDelDia();

            egresosDelDia.Text = totalGastos.ToString();
            ingresosDelDia.Text = totalIngresos.ToString();
            balanceDiario.Text = (totalIngresos - totalGastos).ToString();
        }

        private void ClickGuardarCorte(object sender, RoutedEventArgs e)
        {
            if (CamposCorrectos())
            {
                if (ObtenerEfectivoParaDiaSiguiente())
                {
                    ReporteCaja nuevoReporte = CrearReporteDeCaja();
                    ReporteDeCajaController controlador = new ReporteDeCajaController();

                    if (controlador.RegistrarNuevoReporteDeCaja(nuevoReporte))
                    {
                        MessageBox.Show("El corte de caja se registro con exito");
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error durante el registro");
                    }
                }
            }
        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private bool CamposCorrectos()
        {
            foreach (TextBox campo in listaDeCampos)
            {
                if (!EsNumeroValido(campo.Text))
                {
                    return false;
                }
            }
            return true;
        }

        private bool EsNumeroValido(string texto)
        {
            try
            {
                int numero = int.Parse(texto.Trim());
                if (numero >= 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ingresa unicamente numeros enteros");
                return false;
            }

            return false;
        }

        private bool ObtenerEfectivoParaDiaSiguiente()
        {
            InputDialog dialog = new InputDialog("Ingresa el efectivo para el dia siguiente");
            if (dialog.ShowDialog() == true)
            {
                if (EsNumeroValido(dialog.ResponseText))
                {
                    efectivoDiaSiguiente = double.Parse(dialog.ResponseText);
                    return true;
                }
            }

            return false;
        }

        private ReporteCaja CrearReporteDeCaja()
        {
            double ingresos = double.Parse(ingresosDelDia.Text);
            double egresos = double.Parse(egresosDelDia.Text);
            double balance = double.Parse(balanceDiario.Text);


            return new ReporteCaja()
            {
                BalanceDiario = new Decimal(balance),
                Fecha = DateTime.Now,
                TotalEntrada = new Decimal(ingresos),
                TotalSalida = new Decimal(egresos),
                TotalEfectivoContado = new Decimal(ObtenerSumaTotalDeEfectivo()),
                Observaciones = comentariosInput.Text.Trim(),
                EfectivoDiaSiguiente = new Decimal(efectivoDiaSiguiente),
                GeneradoPor = empleado,
                Guarda = ObtenerListaDeEfectivos()
            };
        }

        private List<Guarda> ObtenerListaDeEfectivos()
        {
            List<Guarda> lista = new List<Guarda>();
            Dictionary<Efectivo, TextBox> listaDeCamposCompletos = ObtenerCantidadesDeEfectivo();

            for (int i = 0; i < listaDeCamposCompletos.Count; i++)
            {
                lista.Add(new Guarda()
                {
                    Efectivo = listaDeCamposCompletos.Keys.ElementAt(i),
                    Cantidad = int.Parse(listaDeCamposCompletos.Values.ElementAt(i).Text)
                }); 
            }

            return lista;
        }

        private Dictionary<Efectivo, TextBox> ObtenerCantidadesDeEfectivo()
        {
            Dictionary<Efectivo, TextBox> diccionario = new Dictionary<Efectivo, TextBox>();

            for (int i = 0; i < listaDeCampos.Count; i++)
            {
                diccionario.Add(listaDeTiposEfectivo.ElementAt(i), listaDeCampos.ElementAt(i));
            }

            return diccionario;
        }

        private double ObtenerSumaTotalDeEfectivo()
        {
            double sumaTotalEfectivo = 0;
            Dictionary<Efectivo, TextBox> diccionario = ObtenerCantidadesDeEfectivo();

            for (int i = 0; i < diccionario.Count ; i++)
            {
                int cantidadEfectivo = int.Parse(diccionario.Values.ElementAt(i).Text);
                double ValorDeEfectivo = double.Parse(diccionario.Keys.ElementAt(i).Valor.ToString());

                sumaTotalEfectivo += (ValorDeEfectivo * cantidadEfectivo);
            }

            return sumaTotalEfectivo;
        }
    }
}