using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class EfectivoDAO
    {
        private PizzasBDEntities conexion;

        public EfectivoDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public List<Efectivo> ObtenerTiposDeEfectivo()
        {
            return conexion.Efectivo.ToList();
        }
    }
}
