using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ReporteDeCajaDAO
    {
        private PizzasBDEntities conexion;

        public ReporteDeCajaDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public bool RegistrarReporteDeCaja(ReporteCaja nuevoReporte)
        {
            bool registrado = false;

            try
            {
                conexion.Entry(nuevoReporte).State = EntityState.Added;
                conexion.SaveChanges();
                registrado = true;
            }
            catch (DbUpdateException ex)
            {
                return registrado;
            }

            return registrado;
        }
    }
}
