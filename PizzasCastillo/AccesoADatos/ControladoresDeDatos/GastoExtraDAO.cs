using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class GastoExtraDAO
    {
        private PizzasBDEntities conexion;

        public GastoExtraDAO()
        {
            conexion = new PizzasBDEntities();
        }

        public bool RegistrarGastoExtra(GastoExtra nuevoGasto)
        {
            bool registrado = false;

            try
            {
                conexion.Entry(nuevoGasto).State = System.Data.Entity.EntityState.Added;
                conexion.SaveChanges();
                registrado = true;
            }
            catch (DbUpdateException)
            {
                return registrado;
            }

            return registrado;
        }

        public List<GastoExtra> ObtenerPedidosDelDia()
        {
            List<GastoExtra> listaRetorno = new List<GastoExtra>();

            try
            {
                DateTime hoy = DateTime.Now;
                int dia = hoy.Day;
                int mes = hoy.Month;
                int anio = hoy.Year;

                listaRetorno = conexion.GastoExtra.Where(x => x.Fecha.Day == dia && x.Fecha.Month == mes && x.Fecha.Year == anio)
                .ToList();
                return listaRetorno;

            }
            catch (Exception)
            {
                return listaRetorno;
            }
        }
    }
}
