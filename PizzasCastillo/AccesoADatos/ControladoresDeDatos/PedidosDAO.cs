using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
   public  class PedidosDAO
    {
        
      
        private List<Pedido> _pedidos = null;
        private Pedido _pedidoEncontrado;
        private const int ACTIVO = 1;
        private const int SIN_CAMBIOS = 0;
        private int _resultado;
        private readonly PizzasBDEntities connection;

        public PedidosDAO()
        {
            connection = new PizzasBDEntities();
            _resultado = 0;
        }




        public void RegistrarPedido(Pedido pedido)
        {


            try { 
                using (PizzasBDEntities connection = new PizzasBDEntities()) 
                {
                    connection.Entry(pedido).State = EntityState.Added;

                    _resultado = connection.SaveChanges();
                }
            } 
            catch (DbUpdateException)
            {

                throw;
            }

           

        }

        public void ActualizarPedidoEstatus(Pedido pedido)
        {

            try
            {
                     Pedido pedidoDB = connection.Pedido.Where(x => x.Id == pedido.Id).SingleOrDefault();

                    pedidoDB.IdEstatusPedido = pedido.IdEstatusPedido;
                   connection.Entry(pedidoDB).State = EntityState.Modified;
                    connection.SaveChanges();

                    _resultado = connection.SaveChanges();
                
            }
            catch (DbUpdateException)
            {

                throw;
            }



        }


        public bool ActualizarPedidoDatos(Pedido pedido) {

            try
            {

              
                    Pedido pedidoDB = connection.Pedido.Where(x => x.Id == pedido.Id).SingleOrDefault();

                    pedidoDB.IdEstatusPedido = pedido.IdEstatusPedido;
                for (int x = 0; x > pedidoDB.Contiene.Count(); x++) 
                    {
                    pedidoDB.Contiene.Remove(pedidoDB.Contiene.Last());
                    }
                    pedidoDB.Contiene = pedido.Contiene;

                    connection.Entry(pedidoDB).State = EntityState.Modified;

                    _resultado = connection.SaveChanges();

             


            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }



     



        public bool ActualizarPedidoContiene(Pedido pedido)
        {

            try
            {

                connection.Entry(pedido).State = EntityState.Modified;

                _resultado = connection.SaveChanges();


            }
            catch (DbUpdateException)
            {

                throw;
            }

            if (_resultado == SIN_CAMBIOS)
            {
                return false;
            }

            return true;
        }



        public List<ArticuloVenta> ObtenerArticulosPedido(Pedido pedidoParabuscar) {

            using (PizzasBDEntities connection = new PizzasBDEntities()) 
            {
                List<Contiene> listaContiene = connection.Contiene.Where(x => x.IdPedido == pedidoParabuscar.Id).ToList();
                List<ArticuloVenta> ArticulosDelPedido = null;
                foreach (Contiene x in listaContiene)
                {
                    ArticulosDelPedido.Add(connection.ArticuloVenta.Where(ar =>ar.CodigoBarra == x.CodigoBarra).FirstOrDefault());
                
                }
                return ArticulosDelPedido;
            
            }
        

        }



        public List<Pedido> ObtenerPedidosEnPreparacion() {
            try
            {
               
                   
                    _pedidos = connection.Pedido.Where(x => x.EstatusPedido.Nombre.Equals
                    ("En Preparacion"))
                    .ToList();
                    return _pedidos;
               
            }
            catch (Exception)
            {
                return _pedidos;
            }
        }
        
        public List<Pedido> ObtenerPedidosDelDia() {
            try
            {

                DateTime hoy = DateTime.Now;
                int dia = hoy.Day;
                int mes = hoy.Month;
                int anio = hoy.Year;
               
                _pedidos = connection.Pedido.Where(x => x.Fecha.Day == dia && x.Fecha.Month==mes && x.Fecha.Year == anio && x.EstatusPedido.Nombre == "pagado")
                .ToList();
                return _pedidos;
               
            }
            catch (Exception e)
            {
                return _pedidos;
            }
        }

        public List<Pedido> ObtenerPedidosEnEspera()
        {
            try
            {
                using (PizzasBDEntities connection = new PizzasBDEntities())
                {

                    _pedidos = connection.Pedido.Where(x => x.EstatusPedido.Nombre.Equals("En Espera")).ToList();
                    return _pedidos;
                }
            }
            catch (Exception)
            {
                return _pedidos;
            }
        }


        public Pedido ObtenerPedidoPorID(int id)
        {
            try
            {


                _pedidoEncontrado = connection.Pedido.Where(x=>x.Id==id).FirstOrDefault();
                return _pedidoEncontrado;
            }
            catch (Exception)
            {
                return _pedidoEncontrado;
            }
        }


    }
}
