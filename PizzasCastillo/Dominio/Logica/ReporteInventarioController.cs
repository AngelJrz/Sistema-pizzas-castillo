using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using Dominio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class ReporteInventarioController
    {
        private const int DISPONIBLE = 1;
        private const int NO_DISPONIBLE = 2;
        private ReporteInventarioDAO reporteDAO;

        public ReporteInventarioController()
        {
            reporteDAO = new ReporteInventarioDAO();
        }

        public bool GuardarReporte(ReporteInventario reporte)
        {

            AccesoADatos.ReporteInventario reporteNuevo = ReporteInventario.CloneToDBEntity(reporte);

            bool seGuardo = false;

            try
            {
                seGuardo = reporteDAO.RegistrarReporte(reporteNuevo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return seGuardo;
        }


        public List<ReporteInventario> ObtenerReportes()
        {
            List<ReporteInventario> listaReportes = new List<ReporteInventario>();
            List<AccesoADatos.ReporteInventario> listaReporteBD = reporteDAO.ObtenerListaReportes();
            foreach (AccesoADatos.ReporteInventario reporteEntity in listaReporteBD)
            {
                ReporteInventario reporteConsultado = new ReporteInventario
                {
                    IdReporte = reporteEntity.IdReporte,
                    Fecha = reporteEntity.Fecha,
                    Nombre = reporteEntity.Nombre,
                    GeneradoPor = Empleado.Clone(reporteEntity.Empleado),
                    Reporta = Reporta.CloneList(reporteEntity.Reporta.ToList())
                };

                listaReportes.Add(reporteConsultado);
            }

            return listaReportes;
        }

        public bool GenerarPDF(ReporteInventario reporte, string ruta)
        {
            bool seGenero;

            GeneradorPDFInventario creador = new GeneradorPDFInventario(reporte);
            seGenero = creador.GenerarPDFReporteInventario(ruta);

            return seGenero;
        }
    }
}
