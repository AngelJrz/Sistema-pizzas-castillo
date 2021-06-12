using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Logica
{
    public class RepartidorController
    {

        public Repartidor BuscarRepartidorPorID(int id) {


            AccesoADatos.ControladoresDeDatos.RepartidorDao repartidorDAO = new AccesoADatos.ControladoresDeDatos.RepartidorDao();
            Repartidor repartidorencontrado = Repartidor.Clone(repartidorDAO.ObtenerRepartidorID(id));
            return repartidorencontrado;
        
        }


        public List<Repartidor> ObtenerRepartidor()
        {


            AccesoADatos.ControladoresDeDatos.RepartidorDao repartidorDAO = new AccesoADatos.ControladoresDeDatos.RepartidorDao();
            List<Repartidor> repartidorencontrado = Repartidor.CloneList(repartidorDAO.ObtenerRepartidor());
            return repartidorencontrado;

        }


    }
}
