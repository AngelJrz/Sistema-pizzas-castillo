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
        public int Estatus { get; set; } = 1;
        public string Status
        {
            get { return Estatus == 1 ? "Activo" : "No Activo"; }
            set { }
        }

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

        public static AccesoADatos.TipoUsuario CloneToEntityDB(Tipo tipoUsuario)
        {
            return new AccesoADatos.TipoUsuario
            {
                Nombre = tipoUsuario.Nombre,
                Estatus = tipoUsuario.Estatus
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

        public static Tipo Clone(AccesoADatos.EstatusPedidoAProveedor tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static List<Tipo> CloneList(List<AccesoADatos.TipoUsuario> tiposUsuario)
        {
            List<Tipo> list = new List<Tipo>();
            tiposUsuario.ToList().ForEach(tipoUsuario => list.Add(
                new Tipo
                {
                    Nombre = tipoUsuario.Nombre,
                    Id = tipoUsuario.Id,
                    Estatus = tipoUsuario.Estatus
                }
            ));
            return list;
        }

        public string ObtenerEtiquetaTipoEmpleado()
        {
            string etiqueta = "";

            switch (Nombre)
            {
                case "Gerente":
                    etiqueta = "GER";
                    break;
                case "Encargado de caja":
                    etiqueta = "CAJ";
                    break;
                case "Contador":
                    etiqueta = "CON";
                    break;
                case "Mesero":
                    etiqueta = "MES";
                    break;
                case "Cocinero":
                    etiqueta = "COC";
                    break;
            }

            return etiqueta;
        }
    }
}
