using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ArticuloVentaDAO
    {
        PizzasBDEntities _connection;
        List<ArticuloVenta> articulosVenta = null;
        public List<ArticuloVenta> ObtenerArticulosVenta() {
            try
            {
                articulosVenta = _connection.ArticuloVenta.ToList();
                return articulosVenta;
            }
            catch (Exception) {

                return articulosVenta;
            }
        }

       
    }
}
