using AccesoADatos;
using AccesoADatos.ControladoresDeDatos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Logica
{
    public class PlatilloController
    {
        public bool GuardarPlatillo(Entidades.Platillo platillo)
        {
            PlatilloDAO platilloDAO = new PlatilloDAO();

            return platilloDAO.RegistrarPlatillo(ClonePlatillo(platillo));
        }

        private AccesoADatos.Platillo ClonePlatillo(Entidades.Platillo platillo)
        {
            return new AccesoADatos.Platillo
            {
                FechaRegisto = platillo.FechaRegisto,
                Receta = platillo.Receta,
                CodigoBarra = platillo.CodigoBarra
            };
        }


    }
}
