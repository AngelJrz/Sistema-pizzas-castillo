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
    
    public partial class Platillo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Platillo()
        {
            this.Consume = new ObservableCollection<Consume>();
        }
    
        public System.DateTime FechaRegisto { get; set; }
        public string Receta { get; set; }
        public string CodigoBarra { get; set; }
    
        public virtual ArticuloVenta ArticuloVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Consume> Consume { get; set; }
    }
}
