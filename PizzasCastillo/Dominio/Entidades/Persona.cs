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
        public TipoUsuario TipoUsuario { get; set; }
        public List<Direccion> Direcciones { get; set; }

        public string NombreCompleto
        {
            get { return Nombres + " " + Apellidos; }
        }

    }
}
