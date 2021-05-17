using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enumeraciones
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estatus { get; set; }

        public static Tipo Clone(AccesoADatos.TipoGasto tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static Tipo Clone(AccesoADatos.TipoPedido tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static Tipo Clone(AccesoADatos.TipoProducto tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static Tipo Clone(AccesoADatos.TipoUsuario tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static Tipo Clone(AccesoADatos.EstatusPedido tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static AccesoADatos.TipoProducto CloneDA(Dominio.Enumeraciones.Tipo tipo)
        {
            return new TipoProducto
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }
        public static Tipo Clone(AccesoADatos.EstatusPedidoAProveedor tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

    }
}
