using Dominio.Entidades;
using Dominio.Logica;
using Dominio.Utilerias;
using Microsoft.Win32;
using Presentacion.Ventanas;
using Presentacion.Ventanas.Usuario;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para EditarPlatillo.xaml
    /// </summary>
    public partial class EditarPlatillo : Page
    {
        byte[] foto;
        string nombreFoto;
        string codigoBarra;
        ObservableCollection<ArticuloVenta> productos;
        private ProductoPopUp productoPopUP;
        private List<ArticuloVenta> insumosList;

        public EditarPlatillo(ArticuloVenta platilloEdicion)
        {
            InitializeComponent();
            ArticuloVentaController ventaController = new ArticuloVentaController();
            productos = new ObservableCollection<ArticuloVenta>();
            insumosList = ventaController.ObtenerProductos();
            foreach (Consume consume in platilloEdicion.Platillo.Consume)
            {
                ArticuloVenta producto = new ArticuloVenta();
                producto = ventaController.ObtenerProducto(consume.Producto.CodigoBarra);
                producto.CantidadLocal = consume.Cantidad;
                productos.Add(producto);
            }

            productoList.ItemsSource = productos;

            foreach (ArticuloVenta p in productos)
            {
                try 
                {
                    File.WriteAllBytes(Recursos.RecursosGlobales.RUTA_IMAGENES + p.NombreFoto, p.Foto);
                }
                catch(System.IO.IOException)
                {

                }
            }

            NombreText.Text = platilloEdicion.Nombre;
            PrecioText.Text = platilloEdicion.Precio.ToString();
            imgPhoto.Source = new BitmapImage(new Uri(Recursos.RecursosGlobales.RUTA_IMAGENES + platilloEdicion.NombreFoto)); ;
            recetaText.Text = platilloEdicion.Platillo.Receta;
            foto = platilloEdicion.Foto;
            nombreFoto = platilloEdicion.NombreFoto;
            codigoBarra = platilloEdicion.CodigoBarra;
        }


        private void agregarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                nombreFoto = op.SafeFileName;
                if (nombreFoto.Length > 150)
                {
                    InteraccionUsuario error = new InteraccionUsuario("Error de nombre", "El nombre de esta imagen es demasiado grande, favor de reducirlo");
                    error.Show();
                }
                else
                {
                    imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    Stream stream = op.OpenFile();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        foto = ms.ToArray();
                    }
                }
            }
        }


        private void Eliminar(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Regresar",
               "Seguro que desea borrar el ingrediente");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                ListBoxItem selectedItem = (ListBoxItem)productoList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
                selectedItem.IsSelected = true;
                productos.Remove((ArticuloVenta)productoList.SelectedItem);
            }
            
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            productoPopUP = new ProductoPopUp(true, insumosList, new ArticuloVenta());
            productoPopUP.RegistroExitoso += new EventHandler(NuevoInsumo);
            productoPopUP.ShowDialog();
        }
        private void NuevoInsumo(object sender, EventArgs e)
        {
            if (productoList.Items.Contains(productoPopUP.productoAgregado))
            {
                InteraccionUsuario error = new InteraccionUsuario("Error de campos", "Este insumo ya esta registrado");
                error.Show();
            }
            else
            {
                productos.Add(productoPopUP.productoAgregado);
                productoPopUP.Close();
            }
        }
        private void ActualizarInsumo(object sender, EventArgs e)
        {
            productos.Add(productoPopUP.productoAgregado);
            productos.Remove((ArticuloVenta)productoList.SelectedItem);
            productoPopUP.Close();
        }
        private void Cancelar(object sender, RoutedEventArgs e)
        {
            Confirmacion dialogoConfirmacion = new Confirmacion("Regresar",
               "Seguro que desea cancelar la actualizacion");

            if (dialogoConfirmacion.ShowDialog() == true)
            {
                NavigationService.GoBack();
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
        public List<Consume> ObtenerProductosConsume(Platillo localPlatillo)
        {
            List<Consume> consumePlatillo = new List<Consume>();
            Consume consumeNew;
            foreach (ArticuloVenta p in productos)
            {
                consumeNew = new Consume
                {
                    Platillo = localPlatillo,
                    Producto = p.Producto,
                    Cantidad = p.CantidadLocal,
                };
                consumePlatillo.Add(consumeNew);
            }

            return consumePlatillo;
        }

        private void Editar(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)productoList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            productoPopUP = new ProductoPopUp(false, insumosList, (ArticuloVenta)productoList.SelectedItem);
            productoPopUP.RegistroExitoso += new EventHandler(ActualizarInsumo);
            productoPopUP.ShowDialog();
        }

        private void Registrar(object sender, RoutedEventArgs e)
        {
            if (nombreFoto == null || foto == null)
            {
                InteraccionUsuario ventana = new InteraccionUsuario("Erro de campos", "No se ha agregado una foto porfavor seleccione una");
                ventana.Show();
            }
            else
            {
                try
                {
                    List<Consume> consumePlatillo = new List<Consume>();
                    Consume consumeNew;

                    ArticuloVenta nuevoPlatillo = new ArticuloVenta
                    {
                        CodigoBarra = codigoBarra,
                        Nombre = NombreText.Text,
                        Precio = Convert.ToDecimal(PrecioText.Text),
                        Foto = foto,
                        Estatus = 1,
                        EsPlatillo = true,
                        NombreFoto = nombreFoto,
                        Platillo = new Platillo
                        {
                            FechaRegisto = DateTime.Now,
                            Receta = recetaText.Text,
                        }
                    };

                    foreach (ArticuloVenta p in productos)
                    {
                        consumeNew = new Consume
                        {
                            Platillo = nuevoPlatillo.Platillo,
                            Producto = p.Producto,
                            Cantidad = p.CantidadLocal,
                        };
                        consumePlatillo.Add(consumeNew);
                    }
                    nuevoPlatillo.Platillo.Consume = consumePlatillo;
                    ValidadorArticuloVenta validadorArticulo = new ValidadorArticuloVenta();
                    if (validadorArticulo.Validar(nuevoPlatillo))
                    {
                        ArticuloVentaController articuloVentaController = new ArticuloVentaController();
                        bool guardado = articuloVentaController.ActualizarPlatilloVenta(nuevoPlatillo);
                        if (guardado)
                        {
                            InteraccionUsuario ventana = new InteraccionUsuario("Exito en registro", "Se ha guardado el platillo con exito");
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
                        InteraccionUsuario ventana = new InteraccionUsuario("Error de Campos", "Uno o mas campos estan vacios y/o incorrectos, verificar porfavor");
                        ventana.Show();

                    }
                }
                catch (Exception ex)
                {
                    InteraccionUsuario ventana = new InteraccionUsuario("Error de registro", "A ocurrido un error de registro");
                    ventana.Show();
                }
            }
        }

    }
}
