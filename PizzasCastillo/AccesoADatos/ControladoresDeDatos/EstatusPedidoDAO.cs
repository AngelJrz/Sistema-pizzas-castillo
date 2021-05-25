using System;
using System.Collections.Generic;
using System.Linq;


namespace AccesoADatos.ControladoresDeDatos
{
   public  class EstatusPedidoDAO
    {
       
        private List<AccesoADatos.EstatusPedido> _estatusPedido;

        public List<AccesoADatos.EstatusPedido> ObtenerTiposPedidos() {

            try
            {
                using (PizzasBDEntities connection = new PizzasBDEntities()) 
                { 
                _estatusPedido = connection.EstatusPedido.ToList();
                
                }
                    
            }
            catch (Exception) {
                return _estatusPedido;
            }
            return _estatusPedido;
        }
    }
}
