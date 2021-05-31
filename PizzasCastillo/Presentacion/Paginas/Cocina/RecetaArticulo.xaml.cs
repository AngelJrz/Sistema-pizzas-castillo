using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
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
