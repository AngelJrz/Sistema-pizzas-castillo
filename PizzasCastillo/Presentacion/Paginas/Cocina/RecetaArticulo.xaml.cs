using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Presentacion.Paginas.Cocina
{
    /// <summary>
    /// Lógica de interacción para RecetaArticulo.xaml
    /// </summary>
    public partial class RecetaArticulo : Page
    {
        public RecetaArticulo(ArticuloVenta platilloEdicion)
        {
            InitializeComponent();
            PlatilloDAO platilloDAO = new PlatilloDAO();
            AccesoADatos.Platillo platillo = platilloDAO.ObtenerPlatillo(platilloEdicion.CodigoBarra);
            recetaText.Text = platillo.Receta;
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
