using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;

namespace Dominio.Logica
{
    public class ReporteDeCajaController
    {
        public bool RegistrarNuevoReporteDeCaja(ReporteCaja nuevoReporte)
        {
            ReporteDeCajaDAO reporteDAO = new ReporteDeCajaDAO();
            return reporteDAO.RegistrarReporteDeCaja(CloneRegisterDominioADatos(nuevoReporte));
        }

        private AccesoADatos.ReporteCaja CloneRegisterDominioADatos(ReporteCaja nuevoReporte)
        {
            return new AccesoADatos.ReporteCaja
            {
                BalanceDiario = nuevoReporte.BalanceDiario,
                Fecha = nuevoReporte.Fecha,
                TotalEntrada = nuevoReporte.TotalEntrada,
                TotalSalida = nuevoReporte.TotalSalida,
                TotalEfectivoContado = nuevoReporte.TotalEfectivoContado,
                Observaciones = nuevoReporte.Observaciones,
                NumeroEmpleado = nuevoReporte.GeneradoPor.NumeroEmpleado,
                EfectivoDiaSiguiente = nuevoReporte.EfectivoDiaSiguiente,
                Guarda = ObtenerListaGuardaDatos(nuevoReporte.Guarda)
            };
        }

        private List<AccesoADatos.Guarda> ObtenerListaGuardaDatos(List<Guarda> listaDominio)
        {
            List<AccesoADatos.Guarda> listaRetorno = new List<AccesoADatos.Guarda>();
            
            for (int i = 0; i < listaDominio.Count; i++)
            {
                listaRetorno.Add(new AccesoADatos.Guarda()
                {
                    IdEfectivo = listaDominio.ElementAt(i).Efectivo.Id,
                    Cantidad = listaDominio.ElementAt(i).Cantidad
                });
            }

            return listaRetorno;
        }
    }
}
