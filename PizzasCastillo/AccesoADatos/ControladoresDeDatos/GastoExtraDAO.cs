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
    }
}
