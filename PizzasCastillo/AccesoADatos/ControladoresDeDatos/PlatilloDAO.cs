using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class PlatilloDAO
    {
        private readonly PizzasBDEntities connection;
        private List<Platillo> platillos;
        private const int SIN_CAMBIOS = 0;

        private int resultado;

        public PlatilloDAO()
        {
            connection = new PizzasBDEntities();
            resultado = 0;
        }

        public List<Platillo> ObtenerPlatillo()
        {
            try
            {
                platillos = connection.Platillo.ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            return platillos;
        }

        public bool RegistrarPlatillo(Platillo platillo)
        {
            try
            {
                connection.Entry(platillo).State = EntityState.Added;
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
