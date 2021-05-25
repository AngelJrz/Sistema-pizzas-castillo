using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class RepartidorDao
    {
        private List<Repartidor> _repartidores;
        private Repartidor repartidorEncontrado;

        public List<Repartidor> ObtenerRepartidor()
        {
            using (PizzasBDEntities _connection = new PizzasBDEntities())
            {
                _repartidores = _connection.Repartidor
                     .Where(repartidor => repartidor.Estatus == 1).ToList();
            }

            return _repartidores;
        }


        public Repartidor ObtenerRepartidorID(int id)
        {
            using (PizzasBDEntities _connection = new PizzasBDEntities())
            {
                repartidorEncontrado = _connection.Repartidor
                     .Where(repartidor => repartidor.Estatus == 1 && repartidor.Id == id).FirstOrDefault();
            }

            return repartidorEncontrado;

        }
    }
}
