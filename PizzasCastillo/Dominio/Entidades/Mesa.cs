namespace Dominio.Entidades
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Estatus { get; set; }

        public static Mesa Clone(AccesoADatos.Mesa mesa)
        {
            return new Mesa
            {
                Id = mesa.IdMesa,
                Estatus = mesa.Estatus
            };
        }
    }
}