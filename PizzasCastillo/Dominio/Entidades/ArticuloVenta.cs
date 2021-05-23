using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ArticuloVenta
    {
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }
        public int Estatus { get; set; }
        public bool EsPlatillo { get; set; }
        public string NombreFoto { get; set; }
        //public decimal CantidadLocal { get; set; }
        public Producto Producto { get; set; }
        public Platillo Platillo { get; set; }

        public static ArticuloVenta Clone(AccesoADatos.ArticuloVenta articuloVenta)
        {
            return new ArticuloVenta
            {
                CodigoBarra = articuloVenta.CodigoBarra,
                Nombre = articuloVenta.Nombre,
                Precio = articuloVenta.Precio,
                Foto = articuloVenta.Foto,
                Estatus = articuloVenta.Estatus,
                EsPlatillo = articuloVenta.EsPlatillo,
                NombreFoto = articuloVenta.NombreFoto
            };
        }

        public static ArticuloVenta CloneProducto(AccesoADatos.ArticuloVenta articuloVenta)
        {
            return new ArticuloVenta
            {
                CodigoBarra = articuloVenta.CodigoBarra,
                Nombre = articuloVenta.Nombre,
                Precio = articuloVenta.Precio,
                Foto = articuloVenta.Foto,
                Estatus = articuloVenta.Estatus,
                EsPlatillo = articuloVenta.EsPlatillo,
                NombreFoto = articuloVenta.NombreFoto,
                Producto = new Producto
                {
                    Cantidad = articuloVenta.Producto.Cantidad,
                    CodigoBarra = articuloVenta.Producto.CodigoBarra,
                    PrecioCompra = articuloVenta.Producto.PrecioCompra,
                    Descripcion = articuloVenta.Producto.Descripcion,
                    Restricciones = articuloVenta.Producto.Restricciones,
                    UnidadDeMedida = articuloVenta.Producto.UnidadDeMedida,
                    Tipo = new Enumeraciones.Tipo
                    {
                        Nombre = articuloVenta.Producto.TipoProducto.Nombre,
                        Estatus = articuloVenta.Producto.TipoProducto.Estatus
                    }
                }
            };
        }

        public static AccesoADatos.ArticuloVenta CloneToDBEntity(ArticuloVenta articulo)
        {
            return new AccesoADatos.ArticuloVenta
            {
                CodigoBarra = articulo.CodigoBarra,
                Nombre = articulo.Nombre,
                Precio = articulo.Precio,
                EsPlatillo = articulo.EsPlatillo,
                Estatus = articulo.Estatus,
                Foto = articulo.Foto,
                NombreFoto = articulo.NombreFoto
            };
        }

        public static AccesoADatos.ArticuloVenta CloneToDBEntityFull(ArticuloVenta articulo)
        {
            return new AccesoADatos.ArticuloVenta
            {
                CodigoBarra = articulo.CodigoBarra,
                Nombre = articulo.Nombre,
                Precio = articulo.Precio,
                EsPlatillo = articulo.EsPlatillo,
                Estatus = articulo.Estatus,
                Foto = articulo.Foto,
                NombreFoto = articulo.NombreFoto,
                Producto = Producto.CloneToDBEntity(articulo.Producto)
            };
        }

        public static List<ArticuloVenta> CloneListProducto(List<AccesoADatos.ArticuloVenta> productos)
        {
            List<ArticuloVenta> listaProductos = new List<ArticuloVenta>();
            productos.ToList().ForEach(articuloVenta => listaProductos.Add(
               new ArticuloVenta
               {
                   CodigoBarra = articuloVenta.CodigoBarra,
                   Nombre = articuloVenta.Nombre,
                   Precio = articuloVenta.Precio,
                   Foto = articuloVenta.Foto,
                   Estatus = articuloVenta.Estatus,
                   EsPlatillo = articuloVenta.EsPlatillo,
                   NombreFoto = articuloVenta.NombreFoto,
                   Producto = new Producto
                   {
                       Cantidad = articuloVenta.Producto.Cantidad,
                       CodigoBarra = articuloVenta.Producto.CodigoBarra,
                       PrecioCompra = articuloVenta.Producto.PrecioCompra,
                       Descripcion = articuloVenta.Producto.Descripcion,
                       Restricciones = articuloVenta.Producto.Restricciones,
                       UnidadDeMedida = articuloVenta.Producto.UnidadDeMedida,
                       Tipo = new Enumeraciones.Tipo
                       {
                           Nombre = articuloVenta.Producto.TipoProducto.Nombre,
                           Estatus = articuloVenta.Producto.TipoProducto.Estatus
                       }
                   }
               }
               ));

            return listaProductos;
        }
    }
}
