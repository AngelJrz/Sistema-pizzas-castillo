using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class ProveedorDAO
    {
        private readonly PizzasBDEntities connection;
        private List<Proveedor> proveedores;
        private const int SIN_CAMBIOS = 0;
        private int resultado;

        public ProveedorDAO()
        {
            connection = new PizzasBDEntities();
            resultado = 0;
        }

        public List<Proveedor> ObtenerProveedores()
        {
            try
            {
                proveedores = connection.Proveedor.ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }


            return proveedores;
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            try
            {
                connection.Entry(proveedor).State = EntityState.Added;
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

        public bool ActualizarProovedor(Proveedor proveedor)
        {
            try
            {
                var proveedorDB = connection.Proveedor.Where(p => p.Id == proveedor.Id).First();
                if (proveedorDB != null)
                {
                    proveedorDB.Nombre = proveedor.Nombre;
                    proveedorDB.NombreEncargado = proveedor.NombreEncargado;
                    proveedorDB.ListaDeProductos = proveedor.ListaDeProductos;
                    proveedorDB.Telefono = proveedor.Telefono;
                    proveedorDB.Dni = proveedor.Dni;
                    proveedorDB.Email = proveedor.Email;
                    proveedorDB.DireccionProveedor.Ciudad = proveedor.DireccionProveedor.Ciudad;
                    proveedorDB.DireccionProveedor.Calle = proveedor.DireccionProveedor.Calle;
                    proveedorDB.DireccionProveedor.CodigoPostal = proveedor.DireccionProveedor.CodigoPostal;
                    proveedorDB.DireccionProveedor.EntidadFederativa = proveedor.DireccionProveedor.EntidadFederativa;
                    proveedorDB.DireccionProveedor.Numero = proveedor.DireccionProveedor.Numero;
                    resultado = connection.SaveChanges();
                }
            }
            catch(DbUpdateException)
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
