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

        public void RegistrarProductosPedido (List<Contiene> productosContiene)
        {
            foreach (Contiene contieneNuevo in productosContiene)
            {

                _connection.Contiene.Add(contieneNuevo);
                _connection.SaveChanges();
            }
        }


        public void EliminarArticuloPedido(string idarticulo, int idpedido)
        {

            try
            {
                Contiene articuloEliminar = new Contiene();
                articuloEliminar.CodigoBarra = idarticulo;
                articuloEliminar.IdPedido = idpedido;
                _connection.Contiene.Remove(articuloEliminar);
                _connection.SaveChanges();
            }
            catch (Exception)
            {

            }


        }
    }
}
