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
        public static Empleado Clone(AccesoADatos.Empleado empleado)
        {
            return new Empleado
            {
                NumeroEmpleado = empleado.NumeroEmpleado,
                Username = empleado.Username,
                Contrasenia = empleado.Contrasenia,
                SalarioQuincenal = empleado.SalarioQuincenal,
                FechaRegistro = empleado.FechaRegistro
            };
        }
    }
}
