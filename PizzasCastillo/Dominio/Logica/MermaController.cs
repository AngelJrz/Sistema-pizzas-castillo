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
