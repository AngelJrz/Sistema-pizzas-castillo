//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoADatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveedor()
        {
            this.PedidoAProveedor = new HashSet<PedidoAProveedor>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string NombreEncargado { get; set; }
        public int IdDireccion { get; set; }
        public byte[] ListaDeProductos { get; set; }
        public string NombreArchivo { get; set; }
    
        public virtual DireccionProveedor DireccionProveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoAProveedor> PedidoAProveedor { get; set; }
    }
}
