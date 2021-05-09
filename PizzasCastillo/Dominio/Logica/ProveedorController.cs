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
        public bool GuardarDireccionCliente(DireccionProveedor direccion, Proveedor proveedor)
        {
            ProveedorDAO proveedorDAO = new ProveedorDAO();

            return proveedorDAO.RegistrarProveedor(CloneDireccionRegister(proveedor));
        }

        private AccesoADatos.Proveedor CloneDireccionRegister(Proveedor proveedor)
        {
            return new AccesoADatos.Proveedor
            {
                Nombre = proveedor.Nombre,
                Dni = proveedor.Dni,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                NombreEncargado = proveedor.NombreEncargado,
                ListaDeProductos = Encoding.ASCII.GetBytes(proveedor.ListaDeProductos)
            };
        }
    }
}
