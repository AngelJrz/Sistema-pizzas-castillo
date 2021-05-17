using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class MermaDAO
    {
        private readonly PizzasBDEntities connection;
        private List<Platillo> platillos;
        private const int SIN_CAMBIOS = 0;

        private int resultado;

        public MermaDAO()
        {
            connection = new PizzasBDEntities();
            resultado = 0;
        }

        public bool RegistrarMermaInsumos(Merma merma)
        {
            try
            {
                connection.Entry(merma).State = EntityState.Added;
                resultado = connection.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;

        }
    }
}
