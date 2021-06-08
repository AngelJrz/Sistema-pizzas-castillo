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

        public double ObtenerSumaDeGastosDelDia()
        {
            decimal sumatoria = 0;
            GastoExtraDAO gastoDAO = new GastoExtraDAO();
            List<AccesoADatos.GastoExtra> listaDeGastos = new List<AccesoADatos.GastoExtra>();
            listaDeGastos = gastoDAO.ObtenerPedidosDelDia();

            foreach (AccesoADatos.GastoExtra gasto in listaDeGastos)
            {
                sumatoria += gasto.Total;
            }

            double total = Convert.ToDouble(sumatoria);

            return total;
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
