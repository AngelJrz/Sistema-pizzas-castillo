using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Empleado : Persona
    {
        public string NumeroEmpleado { get; set; }
        public string Username { get; set; }
        public string Contrasenia { get; set; }
        public decimal SalarioQuincenal { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
