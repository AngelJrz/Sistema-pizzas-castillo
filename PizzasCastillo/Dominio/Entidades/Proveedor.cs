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
                Direccion = DireccionProveedor.Clone(proveedor.DireccionProveedor)
            };
        }
    }


}
