﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PizzasBDEntities : DbContext
    {
        public PizzasBDEntities()
            : base("name=PizzasBDEntities")
        {
        }
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArticuloVenta> ArticuloVenta { get; set; }
        public virtual DbSet<Consume> Consume { get; set; }
        public virtual DbSet<Contiene> Contiene { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<DireccionProveedor> DireccionProveedor { get; set; }
        public virtual DbSet<Efectivo> Efectivo { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EstatusPedido> EstatusPedido { get; set; }
        public virtual DbSet<EstatusPedidoAProveedor> EstatusPedidoAProveedor { get; set; }
        public virtual DbSet<GastoExtra> GastoExtra { get; set; }
        public virtual DbSet<Guarda> Guarda { get; set; }
        public virtual DbSet<Indica> Indica { get; set; }
        public virtual DbSet<Merma> Merma { get; set; }
        public virtual DbSet<Mesa> Mesa { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoAProveedor> PedidoAProveedor { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Platillo> Platillo { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Repartidor> Repartidor { get; set; }
        public virtual DbSet<Reporta> Reporta { get; set; }
        public virtual DbSet<ReporteCaja> ReporteCaja { get; set; }
        public virtual DbSet<ReporteInventario> ReporteInventario { get; set; }
        public virtual DbSet<Solicita> Solicita { get; set; }
        public virtual DbSet<TipoGasto> TipoGasto { get; set; }
        public virtual DbSet<TipoPedido> TipoPedido { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
    }
}
