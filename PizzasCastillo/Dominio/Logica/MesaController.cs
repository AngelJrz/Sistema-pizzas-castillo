using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using static Dominio.Entidades.Mesa;

namespace Dominio.Logica
{
    public class MesaController
    {

      
        private List<AccesoADatos.Mesa> mesasRecuperadas;

        public List<Mesa> ObtenetMesas()
        {

            AccesoADatos.ControladoresDeDatos.MesaDAO mesaDao = new AccesoADatos.ControladoresDeDatos.MesaDAO();
            mesasRecuperadas = mesaDao.ObtenerMesas();

            List<Mesa> mesas = Mesa.CloneList(mesasRecuperadas);
            return mesas;
        }
    }
}
