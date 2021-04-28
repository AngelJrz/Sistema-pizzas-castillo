using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enumeraciones
{
    public enum EstatusPedido
    {
        EnProceso = 1,
        Preparado = 2,
        Entregado = 3,
        Pagado = 4,
        Cancelado = 5
    }
}
