using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class ProveedorController
    {
        public bool GuardarProveedor(Proveedor proveedor)
        {
            ProveedorDAO proveedorDAO = new ProveedorDAO();

            return proveedorDAO.RegistrarProveedor(CloneProveedorRegister(proveedor));
        }

        private AccesoADatos.Proveedor CloneProveedorRegister(Proveedor proveedor)
        {
            return new AccesoADatos.Proveedor
            {
                Nombre = proveedor.Nombre,
                Dni = proveedor.Dni,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                NombreEncargado = proveedor.NombreEncargado,
                ListaDeProductos = Encoding.ASCII.GetBytes(proveedor.ListaDeProductos),
                DireccionProveedor = new AccesoADatos.DireccionProveedor
                {
                    Calle = proveedor.Direccion.Calle,
                    Ciudad = proveedor.Direccion.Ciudad,
                    CodigoPostal = proveedor.Direccion.CodigoPostal,
                    EntidadFederativa = proveedor.Direccion.EntidadFederativa,
                    Numero = proveedor.Direccion.Numero,
                }
            };
        }

        public List<Proveedor> ObtenerProveedores()
        {
            ProveedorDAO proveedorDAO = new ProveedorDAO();
            List<AccesoADatos.Proveedor> proveedoresBD = proveedorDAO.ObtenerProveedores();

            List<Proveedor> proveedores = Proveedor.CloneList(proveedoresBD);

            return proveedores;
        }

        public bool ActualizarProovedor(Proveedor proveedor)
        {
            ProveedorDAO proveedorDAO = new ProveedorDAO();
            AccesoADatos.Proveedor proveedorDA = new AccesoADatos.Proveedor();
            proveedorDA = CloneProveedorRegister(proveedor);
            proveedorDA.Id = proveedor.Id;
            return proveedorDAO.ActualizarProovedor(proveedorDA);

        }
    }
}
