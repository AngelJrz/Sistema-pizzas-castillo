using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using static Dominio.Enumeraciones.Tipo;

namespace Dominio.Logica
{
    public class EstatusPedidoController
    {
        private List<Tipo> tiposPedidos;
       private List<AccesoADatos.EstatusPedido> tiposRecuperados;

        public List<Tipo> ObtenerEstatusPedido() {

            AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO estatusPedidoDAO = new AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO();
           tiposRecuperados= estatusPedidoDAO.ObtenerTiposPedidos();

            foreach (AccesoADatos.EstatusPedido x in tiposRecuperados) {
                tiposPedidos.Add(Clone(x));
            
            
            }
            return tiposPedidos;
        }

    }
}
