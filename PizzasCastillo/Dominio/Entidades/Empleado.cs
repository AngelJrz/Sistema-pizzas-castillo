using Dominio.Enumeraciones;
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

        public static Empleado FullClone(AccesoADatos.Persona empleado)
        {
            AccesoADatos.Empleado empleadoAuxiliar = empleado.Empleado.ToList()[0];
            return new Empleado
            {
                Id = empleado.Id,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                Telefono = empleado.Telefono,
                Email = empleado.Email,
                Estatus = (int)empleado.Estatus,
                TipoUsuario = Tipo.Clone(empleado.TipoUsuario),
                Direcciones = Entidades.Direccion.CloneList(empleado.Direccion.ToList()),
                NumeroEmpleado = empleadoAuxiliar.NumeroEmpleado,
                Username = empleadoAuxiliar.Username,
                Contrasenia = empleadoAuxiliar.Contrasenia,
                SalarioQuincenal = empleadoAuxiliar.SalarioQuincenal,
                FechaRegistro = empleadoAuxiliar.FechaRegistro
            };
        }

        public static List<Empleado> FullCloneList(List<AccesoADatos.Empleado> empleados)
        {
            List<Empleado> list = new List<Empleado>();

            foreach (var empleado in empleados)
            {
                list.Add(new Empleado
                {
                    Id = empleado.Persona.Id,
                    Nombres = empleado.Persona.Nombres,
                    Apellidos = empleado.Persona.Apellidos,
                    Telefono = empleado.Persona.Telefono,
                    Email = empleado.Persona.Email,
                    Estatus = (int)empleado.Persona.Estatus,
                    TipoUsuario = Tipo.Clone(empleado.Persona.TipoUsuario),
                    Direcciones = Entidades.Direccion.CloneList(empleado.Persona.Direccion.ToList()),
                    NumeroEmpleado = empleado.NumeroEmpleado,
                    Username = empleado.Username,
                    Contrasenia = empleado.Contrasenia,
                    SalarioQuincenal = empleado.SalarioQuincenal,
                    FechaRegistro = empleado.FechaRegistro
                });
            }

            return list;
        }

        public static AccesoADatos.Empleado CloneToEntityDB(Empleado empleado)
        {
            return new AccesoADatos.Empleado
            {
                NumeroEmpleado = empleado.NumeroEmpleado,
                Username = empleado.Username,
                Contrasenia = empleado.Contrasenia,
                SalarioQuincenal = empleado.SalarioQuincenal,
                FechaRegistro = empleado.FechaRegistro
            };
        }

        public void SetInformacionPersonal(Persona persona)
        {
            Id = persona.Id;
            Nombres = persona.Nombres;
            Apellidos = persona.Apellidos;
            Telefono = persona.Telefono;
            Email = persona.Email;
            Direcciones = persona.Direcciones;
            TipoUsuario = persona.TipoUsuario;
            Estatus = persona.Estatus;
        }
    }
}
