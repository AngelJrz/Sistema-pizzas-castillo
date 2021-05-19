using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class TipoPedidoDAO
    {
        private List<TipoPedido> _tipoPedido;

        public List<TipoPedido> ObtenerTipoPedido()
        {

            try
            {
                using (PizzasBDEntities connection = new PizzasBDEntities())
                {
                    _tipoPedido = connection.TipoPedido.ToList();

                }

            }
            catch (Exception)
            {
                return _tipoPedido;
            }
            return _tipoPedido;
        }
    }

}

