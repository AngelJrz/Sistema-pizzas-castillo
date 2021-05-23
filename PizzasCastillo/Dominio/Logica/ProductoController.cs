﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using Dominio.Enumeraciones;

namespace Dominio.Logica
{
    public class ProductoController
    {
        private const int DISPONIBLE = 1;
        private const int NO_DISPONIBLE = 2;
        private ProductoDAO productoDAO;

        public ProductoController()
        {
            productoDAO = new ProductoDAO();
        }

        public ResultadoRegistroProducto GuardarProducto(Producto producto)
        {
            if (productoDAO.ObtenerProducto(producto.CodigoBarra) != null)
            {
                return ResultadoRegistroProducto.CodigoBarraDuplicado;
            }

            if (producto.CodigoBarra == null)
            {
                producto.CodigoBarra = AutogenerarCodigoBarra(producto.Nombre, producto.Tipo);
            }

            producto.EsPlatillo = false;
            producto.Estatus = DISPONIBLE;

            AccesoADatos.ArticuloVenta productoNuevo = ArticuloVenta.CloneToDBEntity(producto);
            productoNuevo.Producto = Producto.CloneToDBEntity(producto);

            bool seGuardo;

            try
            {
                seGuardo = productoDAO.RegistrarArticulo(productoNuevo);
            }
            catch (Exception)
            {
                throw;
            }

            if (seGuardo == false)
                return ResultadoRegistroProducto.RegistroFallido;

            return ResultadoRegistroProducto.RegistroExitoso;
        }

        public bool ActualizarProducto(Producto producto)
        {

            AccesoADatos.ArticuloVenta productoNuevo = ArticuloVenta.CloneToDBEntity(producto);
            productoNuevo.Producto = Producto.CloneToDBEntity(producto);

            bool seGuardo;

            try
            {
                seGuardo = productoDAO.ActualizarArticulo(productoNuevo);
            }
            catch (Exception)
            {
                throw;
            }

            return seGuardo;
        }

        public bool ActualizarCantidad(Producto producto)
        {

            AccesoADatos.Producto actualizacion = Producto.CloneToDBEntity(producto);

            bool seGuardo;

            try
            {
                seGuardo = productoDAO.ActualizarCantidad(actualizacion);
            }
            catch (Exception)
            {
                throw;
            }

            return seGuardo;
        }


        public bool EliminarProducto(Producto producto)
        {
            producto.Estatus = NO_DISPONIBLE;
            AccesoADatos.ArticuloVenta productoNuevo = ArticuloVenta.CloneToDBEntity(producto);
            productoNuevo.Producto = Producto.CloneToDBEntity(producto);

            bool seGuardo;

            try
            {
                seGuardo = productoDAO.ActualizarArticulo(productoNuevo);
            }
            catch (Exception)
            {
                throw;
            }

            return seGuardo;
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> listaProcutos = new List<Producto>();
            List<AccesoADatos.ArticuloVenta> productosBD = productoDAO.ObtenerListaProductos();
            foreach (AccesoADatos.ArticuloVenta productoEntity in productosBD)
            {
                Producto productoConsultado = new Producto
                {
                    CodigoBarra = productoEntity.CodigoBarra,
                    Nombre = productoEntity.Nombre,
                    Precio = productoEntity.Precio,
                    Foto = productoEntity.Foto,
                    Estatus = productoEntity.Estatus,
                    EsPlatillo = productoEntity.EsPlatillo,
                    NombreFoto = productoEntity.NombreFoto,
                    Cantidad = productoEntity.Producto.Cantidad,
                    Descripcion = productoEntity.Producto.Descripcion,
                    PrecioCompra = productoEntity.Producto.PrecioCompra,
                    Restricciones = productoEntity.Producto.Restricciones,
                    Tipo = Tipo.Clone(productoEntity.Producto.TipoProducto),
                    UnidadDeMedida = productoEntity.Producto.UnidadDeMedida
                };

                listaProcutos.Add(productoConsultado);
            }

            return listaProcutos;
        }

        public bool IngresarPedidoEntregado(List<Producto> listaProductos)
        {
            List<AccesoADatos.Producto> listaProductosDatos = new List<AccesoADatos.Producto>();

            foreach (Producto producto in listaProductos)
            {
                listaProductosDatos.Add(new AccesoADatos.Producto() {
                    CodigoBarra = producto.CodigoBarra,
                    Cantidad = producto.Cantidad
                });
            }

            ProductosDAO productoDAO = new ProductosDAO();
            bool actualizado = productoDAO.ActualizarInventario(listaProductosDatos);

            return actualizado;
        }

        public List<Producto> ObtenerProductosActivos()
        {
            List<Producto> listaProcutos = new List<Producto>();
            List<AccesoADatos.ArticuloVenta> productosBD = productoDAO.ObtenerListaProductosActivos();
            foreach (AccesoADatos.ArticuloVenta productoEntity in productosBD)
            {
                Producto productoConsultado = new Producto
                {
                    CodigoBarra = productoEntity.CodigoBarra,
                    Nombre = productoEntity.Nombre,
                    Precio = productoEntity.Precio,
                    Foto = productoEntity.Foto,
                    Estatus = productoEntity.Estatus,
                    EsPlatillo = productoEntity.EsPlatillo,
                    NombreFoto = productoEntity.NombreFoto,
                    Cantidad = productoEntity.Producto.Cantidad,
                    Descripcion = productoEntity.Producto.Descripcion,
                    PrecioCompra = productoEntity.Producto.PrecioCompra,
                    Restricciones = productoEntity.Producto.Restricciones,
                    Tipo = Tipo.Clone(productoEntity.Producto.TipoProducto),
                    UnidadDeMedida = productoEntity.Producto.UnidadDeMedida
                };

                listaProcutos.Add(productoConsultado);
            }

            return listaProcutos;
        }

        public AccesoADatos.ArticuloVenta ObtenerArticuloEntity(string codigoBarra)
        {
            List<AccesoADatos.ArticuloVenta> productosBD = productoDAO.ObtenerListaProductos();
             AccesoADatos.ArticuloVenta articuloProducto = 
                productosBD.FirstOrDefault(p => p.CodigoBarra.Equals(codigoBarra) && p.EsPlatillo == false);

            return articuloProducto;
        }

        public List<Producto> BuscarProductosPorNombre(string nombre)
        {
            List<Producto> listaProcutos = new List<Producto>();
            List<AccesoADatos.ArticuloVenta> productosBD = productoDAO.ObtenerProductosPorNombre(nombre);
            foreach (AccesoADatos.ArticuloVenta productoEntity in productosBD)
            {
                Producto productoConsultado = new Producto
                {
                    CodigoBarra = productoEntity.CodigoBarra,
                    Nombre = productoEntity.Nombre,
                    Precio = productoEntity.Precio,
                    Foto = productoEntity.Foto,
                    Estatus = productoEntity.Estatus,
                    EsPlatillo = productoEntity.EsPlatillo,
                    NombreFoto = productoEntity.NombreFoto,
                    Cantidad = productoEntity.Producto.Cantidad,
                    Descripcion = productoEntity.Producto.Descripcion,
                    PrecioCompra = productoEntity.Producto.PrecioCompra,
                    Restricciones = productoEntity.Producto.Restricciones,
                    Tipo = Tipo.Clone(productoEntity.Producto.TipoProducto),
                    UnidadDeMedida = productoEntity.Producto.UnidadDeMedida
                };

                listaProcutos.Add(productoConsultado);
            }

            return listaProcutos;
        }

        public List<Producto> BuscarProductosPorCodigo(string codigo)
        {
            List<Producto> listaProcutos = new List<Producto>();
            List<AccesoADatos.ArticuloVenta> productosBD = productoDAO.ObtenerProductosPorCodigo(codigo);
            foreach (AccesoADatos.ArticuloVenta productoEntity in productosBD)
            {
                Producto productoConsultado = new Producto
                {
                    CodigoBarra = productoEntity.CodigoBarra,
                    Nombre = productoEntity.Nombre,
                    Precio = productoEntity.Precio,
                    Foto = productoEntity.Foto,
                    Estatus = productoEntity.Estatus,
                    EsPlatillo = productoEntity.EsPlatillo,
                    NombreFoto = productoEntity.NombreFoto,
                    Cantidad = productoEntity.Producto.Cantidad,
                    Descripcion = productoEntity.Producto.Descripcion,
                    PrecioCompra = productoEntity.Producto.PrecioCompra,
                    Restricciones = productoEntity.Producto.Restricciones,
                    Tipo = Tipo.Clone(productoEntity.Producto.TipoProducto),
                    UnidadDeMedida = productoEntity.Producto.UnidadDeMedida
                };

                listaProcutos.Add(productoConsultado);
            }

            return listaProcutos;
        }

        private string AutogenerarCodigoBarra(string nombre, Tipo tipo)
        {
            Random azar = new Random();

            string inicio = tipo.ObtenerEtiquetaTipoProducto();
            string codigoGenerado = String.Concat(inicio, nombre.Substring(0, 3).ToUpper());
            codigoGenerado = codigoGenerado + "-" + azar.Next(100, 999);

            return codigoGenerado;
        }
    }
}