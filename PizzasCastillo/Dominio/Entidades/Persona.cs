using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Enumeraciones;

namespace Dominio.Entidades
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Estatus { get; set; } = 1;
        public Tipo TipoUsuario { get; set; }
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();

        public string NombreCompleto
        {
            get { return Nombres + " " + Apellidos; }
        }

        public static Persona Clone(AccesoADatos.Persona persona)
        {
            return new Persona
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Estatus = (int)persona.Estatus,
                TipoUsuario = Tipo.Clone(persona.TipoUsuario),
                Direcciones = Entidades.Direccion.CloneList(persona.Direccion.ToList())
            };
        }

        public static AccesoADatos.Persona CloneToEntityDB(Persona persona)
        {
            return new AccesoADatos.Persona
            {
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Estatus = persona.Estatus,
                IdTipoUsuario = persona.TipoUsuario.Id,
                Direccion = Direccion.CloneToEntityDBList(persona.Direcciones)
            };
        }
    }
}
