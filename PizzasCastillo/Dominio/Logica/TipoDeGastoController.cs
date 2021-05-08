using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class TipoDeGastoController
    {
        public bool GuardarNuevoTipoDeGasto(Tipo nuevoTipoGasto)
        {
            TipoDeGastoDAO tipoGastoDAO = new TipoDeGastoDAO();

            return tipoGastoDAO.RegistrarTipoDeGasto(CloneRegisterDominioADatos(nuevoTipoGasto));
        }

        public List<Tipo> ObtenerTiposDeGasto()
        {
            TipoDeGastoDAO tipoGastoDAO = new TipoDeGastoDAO();
            List<Tipo> listaDeTiposRetorno = new List<Tipo>();

            List<AccesoADatos.TipoGasto> listaDeTiposBD;
            listaDeTiposBD = tipoGastoDAO.ObtenerTiposDeGastos();

            foreach (AccesoADatos.TipoGasto tipoGasto in listaDeTiposBD)
            {
                listaDeTiposRetorno.Add(Tipo.Clone(tipoGasto));
            }

            return listaDeTiposRetorno;
        }

        public bool ModificarTiposDeGasto(int id, string nuevoNombre)
        {
            bool actualizado = false;
            TipoDeGastoDAO tipoGastoDAO = new TipoDeGastoDAO();

            actualizado = tipoGastoDAO.ModificarTipoDeGasto(id, nuevoNombre);

            return actualizado;
        }

        private AccesoADatos.TipoGasto CloneRegisterDominioADatos(Tipo nuevoTipoGasto)
        {
            return new AccesoADatos.TipoGasto
            {
                Nombre = nuevoTipoGasto.Nombre,
                Estatus = 1
            };
        }
    }
}
