using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;
using AccesoADatos.ControladoresDeDatos;

namespace Dominio.Logica
{
    public class TipoProductoController
    {
        public List<Tipo> ObtenerTipoProducto()
        {
            TipoProductoDAO tipoProductoDAO = new TipoProductoDAO();
            List<AccesoADatos.TipoProducto> tiposProductosBD = tipoProductoDAO.ObtenerTipos();

            List<Tipo> tipos = Tipo.CloneList(tiposProductosBD);

            return tipos;
        }
    }
}
