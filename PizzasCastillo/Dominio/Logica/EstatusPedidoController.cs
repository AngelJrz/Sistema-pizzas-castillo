using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class EstatusPedidoController
    {

        public List<AccesoADatos.EstatusPedido> ObtenerEstatusPedido() {

            AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO estatusPedidoDAO = new AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO();
            return estatusPedidoDAO.ObtenerTiposPedidos();
        
        
        }

    }
}
