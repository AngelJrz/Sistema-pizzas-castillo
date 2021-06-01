using Dominio.Entidades;
using Dominio.Logica;
using Presentacion.Ventanas;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Presentacion.Paginas.Finanza
{
    /// <summary>
    /// Lógica de interacción para ListaProveedores.xaml
    /// </summary>
    public partial class ListaProveedores : Page
    {
        readonly ICollectionView provedoresView;

        public ListaProveedores()
        {
            InitializeComponent();
            ProveedorController proveedorController = new ProveedorController();
            List<Proveedor> proveedores = proveedorController.ObtenerProveedores();
            if (proveedores.Count == 0)
            {
                InteraccionUsuario ventana = new InteraccionUsuario("Error", "Aun no se encuentran registrados ningun proovedor");
                ventana.Show();
                NavigationService.GoBack();
            }
            else
            {
                provedoresView = CollectionViewSource.GetDefaultView(proveedores);
                tablaDeProveedores.ItemsSource = provedoresView;
            }
        }


        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClickEditar(object sender, RoutedEventArgs e)
        {
            DataGrid proveedoresGrid = tablaDeProveedores;
            Proveedor proveedorSeleccionado = (Proveedor)proveedoresGrid.SelectedItem;
            NavigationService.Navigate(new EditarProveedor(proveedorSeleccionado));
        }
    }
}
