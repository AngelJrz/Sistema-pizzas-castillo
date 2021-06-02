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
            if (!EstaInformacionCorrecta(reporte))
            {
                return false;
            }

            AccesoADatos.ReporteInventario reporteNuevo = ReporteInventario.CloneToDBEntity(reporte);

            bool seGuardo = false;
            reporteNuevo.Fecha = DateTime.Now;

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
            if (String.IsNullOrEmpty(ruta))
            {
                return false;
            }

            if (!EstaInformacionCorrectaReporte(reporte))
            {
                return false;
            }

            bool seGenero;

            GeneradorPDFInventario creador = new GeneradorPDFInventario(reporte);
            seGenero = creador.GenerarPDFReporteInventario(ruta);

            return seGenero;
        }

        private bool EstaInformacionCorrecta(ReporteInventario reporte)
        {
            if (reporte.GeneradoPor == null)
            {
                return false;
            }

            if (reporte.Reporta == null)
            {
                return false;
            }

            if(reporte.Reporta.Count < 1)
            {
                return false;
            }

            ValidadorReporta validadorReporta = new ValidadorReporta();
            ValidadorReporteInventario validadorReporteInventario = new ValidadorReporteInventario();

            bool estaCorrecto = false;

            foreach (Reporta reportado in reporte.Reporta)
            {
                estaCorrecto = validadorReporta.Validar(reportado);
                if (!estaCorrecto)
                {
                    break;
                }
            }
            return validadorReporteInventario.Validar(reporte) && estaCorrecto;
        }

        private bool EstaInformacionCorrectaReporte(ReporteInventario reporte)
        {
            if (reporte.GeneradoPor == null)
            {
                return false;
            }

            ValidadorReporteInventario validadorReporteInventario = new ValidadorReporteInventario();

            return validadorReporteInventario.Validar(reporte);
        }
    }
}
