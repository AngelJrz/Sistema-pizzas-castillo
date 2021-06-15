using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Ventanas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para ArticulosPedidoMerma.xaml
    /// </summary>
    public partial class ArticulosPedidoMerma : Page
    {
        private Process _teclado;
        private int IdPedido;
        private bool pedidoCompleto = false;
        ArticuloVenta articuloVenta;
        Contiene contieneArticulo;
        public ArticulosPedidoMerma(List<Contiene> contiene, int idPedido)
        {
            InitializeComponent();
            ArticuloVentaController articuloController = new ArticuloVentaController();
            platilloList.ItemsSource = contiene;
            IdPedido = idPedido;

            foreach (Contiene c in contiene)
            {
                try
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + c.ArticuloVenta.NombreFoto, c.ArticuloVenta.Foto);

                }
                catch (System.IO.IOException)
                {

                }
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Regresar",
               "¿Seguro que desea cancelar el registro de esta merma?");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                NavigationService.GoBack();
            }
        }


        private void Registrar(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(justificacionText.Text))
            {
                Merma nuevaMerma = new Merma
                {
                    Pedido = new Dominio.Entidades.Pedido
                    {
                        Id = IdPedido
                    },
                    Fecha = DateTime.Now,
                    Justificacion = justificacionText.Text
                };

                MermaController mermaController = new MermaController();
                bool guardado = mermaController.GuardarMermaPedido(nuevaMerma);

                if (guardado)
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Exito en registro", "Se ha guardado la merma con exito");
                    ventana.Show();
                    NavigationService.GoBack();
                }
                else
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Error de registro", "A ocurrido un error de registro");
                    ventana.Show();
                }
            }
            else
            {
                InteraccionUsuario interaccionUsuario = new InteraccionUsuario("Error de Campos", "Uno o mas campos se encuentran vacios, porfavor ingresar los campos necesarios");
                interaccionUsuario.Show();
            }
        }

        private void MermarArticulo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(justificacionText.Text))
            {
                ListBoxItem selectedItem = (ListBoxItem)platilloList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
                selectedItem.IsSelected = true;
                contieneArticulo = (platilloList.SelectedItem as Contiene);
                articuloVenta = contieneArticulo.ArticuloVenta;
                Merma nuevaMerma = new Merma
                {
                    Fecha = DateTime.Now,
                    Justificacion = justificacionText.Text
                };
                MermaController mermaController = new MermaController();
                bool guardado = mermaController.GuardarMermaArticulosPedido(articuloVenta,contieneArticulo.Cantidad,nuevaMerma);

                if (guardado)
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Exito en registro", "Se ha guardado la merma con exito");
                    ventana.Show();
                    NavigationService.GoBack();
                }
                else
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Error de registro", "A ocurrido un error de registro");
                    ventana.Show();
                }
            }
            else
            {
                InteraccionUsuario interaccionUsuario = new InteraccionUsuario("Error de Campos", "Primero agrege una justificacion a la merma");
                interaccionUsuario.Show();
            }


        }
        private void AbrirTeclado_Touch(object sender, TouchEventArgs e)
        {
            _teclado = Process.Start("osk.exe");

            if (sender.GetType() == typeof(TextBox))
            {
                ((TextBox)sender).Focus();
            }
            else
            {
                ((PasswordBox)sender).Focus();
            }
        }

        private void CerrarTeclado(object sender, RoutedEventArgs e)
        {
            if (_teclado != null)
            {
                if (!_teclado.HasExited)
                    _teclado.Kill();
            }
        }
    }

}
