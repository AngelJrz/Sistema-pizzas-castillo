using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ProductosContienePedidoDAO
    {
        private PizzasBDEntities _connection;
        private List<Pedido> _pedidos;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;

        public bool RegistrarProductosPedido (int idpedido,ArticuloVenta articuloDelPedido,int cantidad, int total)
        {
            try
            {
                Contiene contiene = new Contiene();
                contiene.CodigoBarra = articuloDelPedido.CodigoBarra;
                contiene.Cantidad = cantidad;
                contiene.IdPedido = idpedido;
                contiene.Total = total;
                
                _connection.Contiene.Add(contiene);
                _resultado = _connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }
    }
}
