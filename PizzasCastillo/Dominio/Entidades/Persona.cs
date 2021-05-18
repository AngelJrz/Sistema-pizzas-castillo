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
        public int Estatus { get; set; }
        public Tipo TipoUsuario { get; set; }
        public List<Direccion> Direcciones { get; set; }

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


        public static List<Persona> CloneListPersona(List<AccesoADatos.Persona> personas)
        {
            List<Persona> listaPersonas = new List<Persona>();
            personas.ToList().ForEach(persona => listaPersonas.Add(
                 new Persona
                 {
                     Id = persona.Id,
                     Nombres = persona.Nombres,
                     Apellidos = persona.Apellidos,
                     Telefono = persona.Telefono,
                     Email = persona.Email,
                     Estatus = (int)persona.Estatus,
                     TipoUsuario = Tipo.Clone(persona.TipoUsuario),
                     Direcciones = Entidades.Direccion.CloneList(persona.Direccion.ToList())
                 }));

            return listaPersonas;
        }
    }
            
    }


