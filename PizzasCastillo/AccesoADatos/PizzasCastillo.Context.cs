﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class PizzasBDEntities : DbContext
{
    public PizzasBDEntities()
        : base("name=PizzasBDEntities")
    {

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

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

}

}

