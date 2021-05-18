using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class GastoExtraController
    {
        public bool RegistrarGastoExtra(GastoExtra nuevoGasto)
        {
            GastoExtraDAO gastoDAO = new GastoExtraDAO();
            return gastoDAO.RegistrarGastoExtra(CloneRegisterDominioADatos(nuevoGasto));
        }

        private AccesoADatos.GastoExtra CloneRegisterDominioADatos(GastoExtra nuevoGasto)
        {
            return new AccesoADatos.GastoExtra
            {
                Comentario = nuevoGasto.Comentario,
                Total = nuevoGasto.Total,
                IdTipoGasto = nuevoGasto.Tipo.Id,
                NumeroEmpleado = nuevoGasto.RegistradoPor.NumeroEmpleado,
                Fecha = nuevoGasto.Fecha
            };
        }
    }
}
