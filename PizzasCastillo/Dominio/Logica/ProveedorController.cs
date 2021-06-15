using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class ProveedorController
    {

        public ResultadoRegistro GuardarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            if (proveedor.Direccion == null)
            {
                return ResultadoRegistro.DireccionNoEspecificada;
            }

            if (!EstaInformacionCorrecta(proveedor))
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            ProveedorDAO proveedorDAO = new ProveedorDAO();

            if (proveedorDAO.ObtenerProveedorDNI(proveedor.Dni) != null)
            {
                return ResultadoRegistro.UsuarioYaExiste;
            }

            bool seRegistro;
            try
            {
                seRegistro = proveedorDAO.RegistrarProveedor(CloneProveedorRegister(proveedor));
            }
            catch (Exception)
            {
                throw;
            }

            if (!seRegistro)
            {
                return ResultadoRegistro.RegistroFallido;

            }
            else
            {
                return ResultadoRegistro.RegistroExitoso;
            }
        }

        private bool EstaInformacionCorrecta(Proveedor proveedor)
        {
            ValidadorProveedor validadorProveedor = new ValidadorProveedor();
            ValidadorDireccionProveedor validadorDireccion = new ValidadorDireccionProveedor();

            return validadorProveedor.Validar(proveedor) &&
                validadorDireccion.Validar(proveedor.Direccion);
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
                ListaDeProductos = proveedor.ListaDeProductos,
                DireccionProveedor = new AccesoADatos.DireccionProveedor
                {
                    Calle = proveedor.Direccion.Calle,
                    Ciudad = proveedor.Direccion.Ciudad,
                    CodigoPostal = proveedor.Direccion.CodigoPostal,
                    EntidadFederativa = proveedor.Direccion.EntidadFederativa,
                    Numero = proveedor.Direccion.Numero,
                },
                NombreArchivo = proveedor.NombreArchivo
            };
        }

        public List<Proveedor> ObtenerProveedores()
        {
            ProveedorDAO proveedorDAO = new ProveedorDAO();
            List<AccesoADatos.Proveedor> proveedoresBD = proveedorDAO.ObtenerProveedores();

            List<Proveedor> proveedores = Proveedor.CloneList(proveedoresBD);

            return proveedores;
        }

        public ResultadoRegistro ActualizarProovedor(Proveedor proveedor, bool dniNew)
        {
            

            if (proveedor == null)
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            if (proveedor.Direccion == null)
            {
                return ResultadoRegistro.DireccionNoEspecificada;
            }

            if (!EstaInformacionCorrecta(proveedor))
            {
                return ResultadoRegistro.InformacionIncorrecta;
            }

            ProveedorDAO proveedorDAO = new ProveedorDAO();
            if (dniNew == true)
            {
                if (proveedorDAO.ObtenerProveedorDNI(proveedor.Dni) != null)
                {
                    return ResultadoRegistro.UsuarioYaExiste;
                }
            }
            

            bool seRegistro;
            AccesoADatos.Proveedor proveedorDA = new AccesoADatos.Proveedor();
            try
            {
                proveedorDA = CloneProveedorRegister(proveedor);
                proveedorDA.Id = proveedor.Id;
                seRegistro = proveedorDAO.ActualizarProovedor(proveedorDA);
            }
            catch (Exception ex)
            {
                throw;
            }

            if (!seRegistro)
            {
                return ResultadoRegistro.RegistroFallido;

            }
            else
            {
                return ResultadoRegistro.RegistroExitoso;
            }

        }
    }
}
