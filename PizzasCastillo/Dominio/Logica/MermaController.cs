using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class MermaController
    {
        public bool GuardarMermaInsumos(Merma nuevaMerma)
        {
            MermaDAO mermaDAO = new MermaDAO();
            return mermaDAO.RegistrarMermaInsumos(Clone(nuevaMerma));
        }

        public bool GuardarMermaArticulosPedido(ArticuloVenta articuloVenta,int cantidad, Merma nuevaMerma)
        {
            MermaDAO mermaDAO = new MermaDAO();
            ProductoDAO producto = new ProductoDAO();
            List<Consume> consume = new List<Consume>();
            consume = Consume.CloneList(producto.ConsumePlatillo(articuloVenta.CodigoBarra));

            List<Indica> indicaList = new List<Indica>();
            foreach (Consume con in consume)
            {
                indicaList.Add(new Indica
                {
                    Cantidad = (con.Cantidad * cantidad),
                    Producto = con.Producto
                });
            }
            nuevaMerma.Indica = indicaList;
            bool guardado = mermaDAO.RegistrarMermaInsumos(Clone(nuevaMerma));
            return guardado;
        }

        public bool GuardarMermaPedido(Merma nuevaMerma)
        {
            MermaDAO mermaDAO = new MermaDAO();
            return mermaDAO.RegistrarMermaPedido(CloneMermaPedido(nuevaMerma));
        }

        public static AccesoADatos.Merma CloneMermaPedido(Merma merma)
        {
            return new AccesoADatos.Merma
            {
                IdPedido = merma.Pedido.Id,
                Fecha = merma.Fecha,
                Justificacion = merma.Justificacion,
            };
        }

        public static AccesoADatos.Merma Clone(Merma merma)
        {
            return new AccesoADatos.Merma
            {
                Id = merma.Id,
                Fecha = merma.Fecha,
                Justificacion = merma.Justificacion,
                Indica = Entidades.Indica.CloneListDA(merma.Indica.ToList()),
            };
        }
    }
}
