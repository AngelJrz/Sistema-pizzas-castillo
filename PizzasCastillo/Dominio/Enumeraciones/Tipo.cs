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

        public static AccesoADatos.TipoProducto CloneToEntityDB(Tipo tipoProducto)
        {
            return new AccesoADatos.TipoProducto
            {
                Nombre = tipoProducto.Nombre,
                Estatus = tipoProducto.Estatus,
                Id = tipoProducto.Id
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


        public static Tipo Clone(AccesoADatos.EstatusPedidoAProveedor tipo)
        {
            return new Tipo
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Estatus = tipo.Estatus
            };
        }

        public static List<Tipo> CloneList(List<AccesoADatos.TipoProducto> tipos)
        {
            List<Tipo> list = new List<Tipo>();
            tipos.ToList().ForEach(tipoProducto => list.Add(
                new Tipo
                {
                    Nombre = tipoProducto.Nombre,
                    Id = tipoProducto.Id,
                    Estatus = tipoProducto.Estatus
                }
            ));
            return list;
        }

        public string ObtenerEtiquetaTipoProducto()
        {
            string etiqueta = "";

            switch (Nombre)
            {
                case "Final":
                    etiqueta = "FIN";
                    break;
                case "Insumo":
                    etiqueta = "INS";
                    break;
            }

            return etiqueta;
        }
    }
}
