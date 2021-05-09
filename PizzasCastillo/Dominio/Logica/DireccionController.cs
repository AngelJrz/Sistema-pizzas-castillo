using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class DireccionController
    {
        public bool GuardarDireccionCliente(Direccion direccion,string telefono)
        {
            DireccionDAO direccionDAO = new DireccionDAO();

            return direccionDAO.RegistrarDireccionCliente(CloneDireccionRegister(direccion), telefono);
        }

        private AccesoADatos.Direccion CloneDireccionRegister(Direccion direccion)
        {
            return new AccesoADatos.Direccion
            {
                Calle = direccion.Calle,
                Ciudad = direccion.Ciudad,
                CodigoPostal = direccion.CodigoPostal,
                NumeroInterior = direccion.NumeroInterior,
                Referencias = direccion.Referencias,
                NumeroExterior = direccion.NumeroExterior,
                EntidadFederativa = direccion.EntidadFederativa,
                Colonia = direccion.Colonia
            };
        }

        private AccesoADatos.DireccionProveedor CloneDireccionProveedor(DireccionProveedor direccion)
        {
            return new AccesoADatos.DireccionProveedor
            {
                Calle = direccion.Calle,
                Ciudad = direccion.Ciudad,
                CodigoPostal = direccion.CodigoPostal,
                EntidadFederativa = direccion.EntidadFederativa,
                Numero = direccion.Numero
            };
        }

        public bool GuardarDireccionProveedor(DireccionProveedor direccion)
        {
            DireccionDAO direccionDAO = new DireccionDAO();

            return direccionDAO.RegistrarDireccionProveedor(CloneDireccionProveedor(direccion));
        }
    }
}