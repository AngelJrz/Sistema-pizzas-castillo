using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string NombreEncargado { get; set; }
        public string ListaDeProductos { get; set; }
        public DireccionProveedor Direccion { get; set; }
        public static Proveedor Clone(AccesoADatos.Proveedor proveedor)
        {
            return new Proveedor
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Dni = proveedor.Dni,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                NombreEncargado = proveedor.NombreEncargado,
                ListaDeProductos = BitConverter.ToString(proveedor.ListaDeProductos),
                Direccion = DireccionProveedor.Clone(proveedor.DireccionProveedor)
            };
        }

        public static List<Proveedor> CloneList(List<AccesoADatos.Proveedor> proveedores)
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            proveedores.ToList().ForEach(proveedor => listaProveedores.Add(
               new Proveedor
               {
                   Id = proveedor.Id,
                   Nombre = proveedor.Nombre,
                   Dni = proveedor.Dni,
                   Email = proveedor.Email,
                   Telefono = proveedor.Telefono,
                   NombreEncargado = proveedor.NombreEncargado,
                   ListaDeProductos = BitConverter.ToString(proveedor.ListaDeProductos),
                   Direccion = DireccionProveedor.Clone(proveedor.DireccionProveedor)
               }
               ));

            return listaProveedores;
        }
    }


}
