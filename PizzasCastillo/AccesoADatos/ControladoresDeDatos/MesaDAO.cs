using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class MesaDAO
    {
        private List<Mesa> mesasEncontradas;
       
        public List<Mesa> ObtenerMesas()
        {
            try
            {
                using (PizzasBDEntities connection = new PizzasBDEntities())
                {
                    mesasEncontradas = connection.Mesa.Where(m => m.Estatus == 1).ToList();
                }
            }
            catch (Exception)
            {
                throw;

            }
            return mesasEncontradas;
        }

    }
}
