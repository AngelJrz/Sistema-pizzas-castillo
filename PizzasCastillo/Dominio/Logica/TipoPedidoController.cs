using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Enumeraciones.Tipo;

namespace Dominio.Logica
{
    public class TipoPedidoController
    {
       
        

        public List<Tipo> ObtenerTipoPedido()
        {

            AccesoADatos.ControladoresDeDatos.TipoPedidoDAO tipoPedidoDAO = new AccesoADatos.ControladoresDeDatos.TipoPedidoDAO();
            List<AccesoADatos.TipoPedido> tiposRecuperados = tipoPedidoDAO.ObtenerTipoPedido();
            List<Tipo> tipoPedido = Tipo.CloneList(tiposRecuperados);
            return tipoPedido;


          
        }


    }
}
