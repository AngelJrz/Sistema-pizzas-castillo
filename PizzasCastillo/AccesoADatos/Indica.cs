//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoADatos
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Indica
    {
        public int IdMerma { get; set; }
        public string CodigoBarra { get; set; }
        public decimal Cantidad { get; set; }
    
        public virtual Merma Merma { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
