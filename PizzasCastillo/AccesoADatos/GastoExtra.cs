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
    
    public partial class GastoExtra
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public decimal Total { get; set; }
        public int IdTipoGasto { get; set; }
        public string NumeroEmpleado { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual TipoGasto TipoGasto { get; set; }
    }
}
