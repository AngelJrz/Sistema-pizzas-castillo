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
            List<Tipo> listaDeTiposRetorno = new List<Tipo>();
            List<AccesoADatos.TipoGasto> listaDeTiposBD;
            TipoDeGastoDAO tipoGastoDAO = new TipoDeGastoDAO();

            listaDeTiposBD = tipoGastoDAO.ObtenerTiposDeGastos();
            foreach (AccesoADatos.TipoGasto tipoGasto in listaDeTiposBD)
            {
                Tipo tipoLista = new Tipo()
                {
                    Id = tipoGasto.Id,
                    Nombre = tipoGasto.Nombre,
                    Estatus = tipoGasto.Estatus
                };

                listaDeTiposRetorno.Add(tipoLista);
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
