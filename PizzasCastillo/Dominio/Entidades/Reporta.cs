namespace Dominio.Entidades
{
    public class Reporta
    {
        public decimal CantidadEnInventario { get; set; }
        public decimal CantidadReal { get; set; }
        public string Comentario { get; set; }
        public Producto Producto { get; set; }
    }
}