using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;


namespace Dominio.Logica
{
    public class EstatusPedidoController
    {
        
       private List<AccesoADatos.EstatusPedido> tiposRecuperados;

        public List<Tipo> ObtenerEstatusPedido() {
            List<Tipo> tiposPedidos = new List<Tipo>();
            AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO estatusPedidoDAO = new AccesoADatos.ControladoresDeDatos.EstatusPedidoDAO();
           tiposRecuperados= estatusPedidoDAO.ObtenerTiposPedidos();

            foreach (AccesoADatos.EstatusPedido x in tiposRecuperados) {
                tiposPedidos.Add(Tipo.Clone(x));
            
            
            }
            return tiposPedidos;
        }

    }
}
