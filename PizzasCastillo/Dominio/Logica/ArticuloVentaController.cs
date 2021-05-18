using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class ArticuloVentaController
    {
        public const string CLAVE_PLATILLO = "PLAT-";
        public bool GuardarPlatilloVenta(ArticuloVenta articuloVenta)
        {
            ArticuloVentaDAO articuloVentaDAO = new ArticuloVentaDAO();

            return articuloVentaDAO.RegistrarArticuloVenta(CloneArticuloPlatillo(articuloVenta));
        }

        private AccesoADatos.ArticuloVenta CloneArticuloPlatillo(ArticuloVenta articuloVenta)
        {
            List<AccesoADatos.Consume> consumesCollection = new List<AccesoADatos.Consume>();
            ArticuloVentaDAO articuloVentaDAO = new ArticuloVentaDAO();
            int numeroPlatillos = articuloVentaDAO.ObtenerPlatillos().Count;
            string clavePlatillo = CLAVE_PLATILLO;
            if (numeroPlatillos < 9)
            {
                numeroPlatillos += 1;
                clavePlatillo = CLAVE_PLATILLO + "0000" + numeroPlatillos;
            }
            else
            {
                if (numeroPlatillos < 99)
                {
                    numeroPlatillos += 1;
                    clavePlatillo = CLAVE_PLATILLO + "000" + numeroPlatillos;
                }
                else
                {
                    if (numeroPlatillos < 999)
                    {
                        numeroPlatillos += 1;
                        clavePlatillo = CLAVE_PLATILLO + "00" + numeroPlatillos;
                    }
                    else
                    {
                        if (numeroPlatillos < 9999)
                        {
                            numeroPlatillos += 1;
                            clavePlatillo = CLAVE_PLATILLO + "0" + numeroPlatillos;
                        }
                        else
                        {
                            numeroPlatillos += 1;
                            clavePlatillo = CLAVE_PLATILLO + numeroPlatillos;
                        }
                    }
                }
            }

            foreach (Consume c in articuloVenta.Platillo.Consume)
            {
                consumesCollection.Add(new AccesoADatos.Consume
                {
                    Cantidad = c.Cantidad,
                    PlatilloCodigoBarra = clavePlatillo,
                    ProductoCodigoBarra = c.Producto.CodigoBarra
                });
            }

            return new AccesoADatos.ArticuloVenta
            {
                CodigoBarra = clavePlatillo,
                Nombre = articuloVenta.Nombre,
                Precio = articuloVenta.Precio,
                Foto = articuloVenta.Foto,
                Estatus = articuloVenta.Estatus,
                EsPlatillo = articuloVenta.EsPlatillo,
                NombreFoto = articuloVenta.NombreFoto,
                Platillo = new AccesoADatos.Platillo
                {
                    CodigoBarra = clavePlatillo,
                    FechaRegisto = DateTime.Now,
                    Receta = articuloVenta.Platillo.Receta,
                    Consume = consumesCollection
                },
            };
        }

        public List<ArticuloVenta> ObtenerProductos()
        {
            ArticuloVentaDAO articuloDAO = new ArticuloVentaDAO();
            List<AccesoADatos.ArticuloVenta> productosBD = articuloDAO.ObtenerProductos();

            List<ArticuloVenta> articulos = ArticuloVenta.CloneListProducto(productosBD);

            return articulos;
        }

        public List<ArticuloVenta> ObtenerProductosNombre(string nombre)
        {
            ArticuloVentaDAO articuloDAO = new ArticuloVentaDAO();
            List<AccesoADatos.ArticuloVenta> productosBD = articuloDAO.ObtenerArticuloNombre(nombre);

            List<ArticuloVenta> articulos = ArticuloVenta.CloneListProducto(productosBD);

            return articulos;
        }
    }
}
