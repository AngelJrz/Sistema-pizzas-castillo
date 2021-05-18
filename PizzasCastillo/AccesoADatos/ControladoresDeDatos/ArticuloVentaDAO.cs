using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ArticuloVentaDAO
    {
        private readonly PizzasBDEntities connection;
        private List<ArticuloVenta> articulosVenta;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int resultado;


        public ArticuloVentaDAO()
        {
            connection = new PizzasBDEntities();
            resultado = 0;
        }

        public List<ArticuloVenta> ObtenerPlatillos()
        {
            try
            {
                articulosVenta = connection.ArticuloVenta
                .Where(articulo => articulo.EsPlatillo == true)
                .ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            return articulosVenta;
        }

        public List<ArticuloVenta> ObtenerProductos()
        {
            try
            {
                articulosVenta = connection.ArticuloVenta
                .Where(articulo => articulo.EsPlatillo == false)
                .ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            return articulosVenta;
        }

        public bool RegistrarArticuloVenta(ArticuloVenta articuloVenta)
        {
            try
            {
                connection.Entry(articuloVenta).State = EntityState.Added;
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



        public List<ArticuloVenta> ObtenerArticuloNombre(string nombre)
        {
            try {
                articulosVenta = connection.ArticuloVenta
                       .Where(lista => lista.Nombre.Contains(nombre)).ToList();

            }
            catch (ArgumentNullException) {
                throw;
            }
            return articulosVenta;
        }

    }
}
