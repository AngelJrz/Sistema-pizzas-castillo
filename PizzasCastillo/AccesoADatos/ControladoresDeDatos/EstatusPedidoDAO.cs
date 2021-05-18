using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
   public  class EstatusPedidoDAO
    {
        private PizzasBDEntities _connection;
        private List<AccesoADatos.EstatusPedido> _estatusPedido = null;

        public List<AccesoADatos.EstatusPedido> ObtenerTiposPedidos() {

            try
            {
                _estatusPedido = _connection.EstatusPedido.ToList();
            }
            catch (Exception) {
                return _estatusPedido;
            }
            return _estatusPedido;
        }
    }
}
