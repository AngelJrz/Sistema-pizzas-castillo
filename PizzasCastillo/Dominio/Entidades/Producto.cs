using Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Producto : ArticuloVenta
    {
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public string Restricciones { get; set; }
        public Tipo Tipo { get; set; }
        public string UnidadDeMedida { get; set; }

        public static Producto Clone(AccesoADatos.Producto producto)
        {
            return new Producto
            {
                Cantidad = producto.Cantidad,
                Descripcion = producto.Descripcion,
                PrecioCompra = producto.PrecioCompra,
                Restricciones = producto.Restricciones,
                Tipo = Tipo.Clone(producto.TipoProducto),
                UnidadDeMedida = producto.UnidadDeMedida
            };
        }

        public static AccesoADatos.Producto CloneToDBEntity(Producto producto)
        {
            return new AccesoADatos.Producto
            {
                CodigoBarra = producto.CodigoBarra,
                Cantidad = producto.Cantidad,
                Descripcion = producto.Descripcion,
                PrecioCompra = producto.PrecioCompra,
                Restricciones = producto.Restricciones,
                IdTipoProducto = producto.Tipo.Id,
                UnidadDeMedida = producto.UnidadDeMedida
            };
        }

        public static AccesoADatos.Producto CloneToDBEntityFull(AccesoADatos.ArticuloVenta producto)
        {
            return new AccesoADatos.Producto
            {
                ArticuloVenta = producto,
                CodigoBarra = producto.CodigoBarra,
                Cantidad = producto.Producto.Cantidad,
                Descripcion = producto.Producto.Descripcion,
                PrecioCompra = producto.Producto.PrecioCompra,
                Restricciones = producto.Producto.Restricciones,
                IdTipoProducto = producto.Producto.IdTipoProducto,
                UnidadDeMedida = producto.Producto.UnidadDeMedida
            };
        }

        public void SetDatosArticulo(ArticuloVenta articulo)
        {
            CodigoBarra = articulo.CodigoBarra;
            Nombre = articulo.Nombre;
            Precio = articulo.Precio;
            Foto = articulo.Foto;
            Estatus = articulo.Estatus;
            EsPlatillo = false;
        }
    }
}
