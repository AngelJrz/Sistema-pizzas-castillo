using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class EfectivoController
    {
        public List<Efectivo> ObtenerTiposDeEfectivo()
        {
            EfectivoDAO efectivoDAO = new EfectivoDAO();
            List<Efectivo> listaARetornar = CloneDatosADominio(efectivoDAO.ObtenerTiposDeEfectivo());
            return listaARetornar;
        }

        private List<Efectivo> CloneDatosADominio(List<AccesoADatos.Efectivo> listaDeTipos)
        {
            List<Efectivo> listaDeDominio = new List<Efectivo>();
            foreach (AccesoADatos.Efectivo tipo in listaDeTipos)
            {
                listaDeDominio.Add(Efectivo.Clone(tipo));
            }

            return listaDeDominio;
        }
    }
}
