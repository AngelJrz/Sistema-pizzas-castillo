using Dominio.Enumeraciones;

namespace Dominio.Entidades
{
    public class Guarda
    {
        public int Cantidad { get; set; }
        public Efectivo Efectivo { get; set; }

        public static Guarda Clone(AccesoADatos.Guarda guarda)
        {
            return new Guarda
            {
                Cantidad = guarda.Cantidad,
                Efectivo = Efectivo.Clone(guarda.Efectivo)
            };
        }
    }
}