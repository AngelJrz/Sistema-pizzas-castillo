using AccesoADatos.ControladoresDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Entidades.Contiene;
using static Dominio.Enumeraciones.PedidosResult;


using Dominio.Entidades;
namespace Dominio.Logica
{
    public class PedidoController
    {
      

        public ResultsPedidos AgregarPedido(Pedido pedido)
        {
            ResultsPedidos resultado = ResultsPedidos.NoSePudoregistrar;
            bool resultadoDao = false;
            PedidosDAO dao = new PedidosDAO();


            if (pedido.Tipo.Id == 1)
            {
               resultadoDao= dao.RegistrarPedido(CloneDominioADatosParaLlevar(pedido));
                if (resultadoDao)
                {
                    resultado = ResultsPedidos.RegistradoConExito;
                }
                else {
                    resultado = ResultsPedidos.NoSePudoregistrar;
                }
          
            }
            else 
            {
                resultadoDao =dao.RegistrarPedido(CloneDominioADatosLocal(pedido));
                if (resultadoDao)
                {
                    resultado = ResultsPedidos.RegistradoConExito;
                }
                else
                {
                    resultado = ResultsPedidos.NoSePudoregistrar;
                }
           
            }

            return resultado;
        }

        public ResultsPedidos ActualizarPedidoEstatus(Pedido pedido)
        {
            PedidosDAO dao = new PedidosDAO();
            bool resultadoActualizacion = false;
            ResultsPedidos resultado = ResultsPedidos.NoSePudoActualizar;

            if (pedido.Tipo.Id == 1)
            {
                resultadoActualizacion= dao.ActualizarPedidoEstatus(CloneDominioADatosParaLlevarEditar(pedido));
                if (resultadoActualizacion)
                {
                    resultado = ResultsPedidos.ActualizadoConExito;
                }
                else
                {
                    resultado = ResultsPedidos.NoSePudoActualizar;
                }
            }
            else
            {
                resultadoActualizacion= dao.ActualizarPedidoEstatus(CloneDominioADatosLocalEditar(pedido));
                if (resultadoActualizacion)
                {
                    resultado = ResultsPedidos.ActualizadoConExito;
                }
                else
                {
                    resultado = ResultsPedidos.NoSePudoActualizar;
                }
            }
            return resultado;

        }

         public bool ActualizarPedidoEstatusPreparado(Pedido pedido)
        {
            PedidosDAO dao = new PedidosDAO();
            if(dao.ObtenerPedidoPorID(pedido.Id) == null)
            {
                return false;
            }
            if (pedido.Tipo.Id == 1)
            {
                return dao.ActualizarPedidoEstatus(CloneDominioADatosParaLlevarEditar(pedido));
            }
            else
            {
                return dao.ActualizarPedidoEstatus(CloneDominioADatosLocalEditar(pedido));
            }

            if (!pedido.Estatus.Nombre.Equals("Entregado"))
            {
                return false;
            }

            EstatusPedidoController estatusController = new EstatusPedidoController();
            List<Dominio.Enumeraciones.Tipo> listaTipos = estatusController.ObtenerEstatusPedido();
            pedido.Estatus = listaTipos.Find(t => t.Nombre.Equals("Pagado"));
            bool resultado = false;
            resultado = dao.ActualizarPedidoEstatus(CloneDominioADatosLocalEditar(pedido));

            return resultado;
        }

        public bool ActualizarAPagado(Pedido pedido, decimal pago)
        {

            if (pedido.Total > pago)
            {
                return false;
            }

            if (!pedido.Estatus.Nombre.Equals("Entregado"))
            {
                return false;
            }

            PedidosDAO dao = new PedidosDAO();
            EstatusPedidoController estatusController = new EstatusPedidoController();
            List<Dominio.Enumeraciones.Tipo> listaTipos = estatusController.ObtenerEstatusPedido();
            pedido.Estatus = listaTipos.Find(t => t.Nombre.Equals("Pagado"));
            bool resultado = false;
            resultado = dao.ActualizarPedidoEstatus(CloneDominioADatosLocalEditar(pedido));

            return resultado;
        }

        public ResultsPedidos ActualizarPedidoArticulos(Pedido pedido)
        {
            PedidosDAO dao = new PedidosDAO();
            bool resultadoActualizacion = false;
            ResultsPedidos resultado = ResultsPedidos.NoSePudoActualizar;
            if (pedido.Tipo.Id == 1)
            {
                resultadoActualizacion =dao.ActualizarPedidoDatos(CloneDominioADatosParaLlevarEditar(pedido));
                if (resultadoActualizacion)
                {
                    resultado = ResultsPedidos.ActualizadoConExito;
                }
                else {
                    resultado = ResultsPedidos.NoSePudoActualizar;
                }
              
            }
            else
            {
                resultadoActualizacion=dao.ActualizarPedidoDatos(CloneDominioADatosLocalEditar(pedido));
                if (resultadoActualizacion) 
                {
                    resultado = ResultsPedidos.ActualizadoConExito;
                }
                else
                {
                    resultado = ResultsPedidos.NoSePudoActualizar;
                }
            }

            return resultado;
        }




        public AccesoADatos.Pedido CloneDominioADatosParaLlevar(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
               
                Fecha = pedidoAClonar .Fecha,
                Total = pedidoAClonar.Total,
                IdPersona=pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido=pedidoAClonar.Estatus.Id,
                IdTipoPedido=pedidoAClonar.Tipo.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdRepartidor = pedidoAClonar.RepartidoPor.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }


        public AccesoADatos.Pedido CloneDominioADatosParaLlevarEditar(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
                Id = pedidoAClonar.Id,
                Fecha = pedidoAClonar.Fecha,
                Total = pedidoAClonar.Total,
                IdPersona = pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido = pedidoAClonar.Estatus.Id,
                IdTipoPedido = pedidoAClonar.Tipo.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdRepartidor = pedidoAClonar.RepartidoPor.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }


        public AccesoADatos.Pedido CloneDominioADatosLocalEditar(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
                Id=pedidoAClonar.Id,
                Fecha = pedidoAClonar.Fecha,
                Total = pedidoAClonar.Total,
                IdPersona = pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido = pedidoAClonar.Estatus.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdTipoPedido = pedidoAClonar.Tipo.Id,
                IdMesa = pedidoAClonar.Mesa.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }

        public AccesoADatos.Pedido CloneDominioADatosLocal(Pedido pedidoAClonar)
        {
            return new AccesoADatos.Pedido()
            {
                Fecha = pedidoAClonar.Fecha,
                Total = pedidoAClonar.Total,
                IdPersona = pedidoAClonar.SolicitadoPor.Id,
                IdEstatusPedido = pedidoAClonar.Estatus.Id,
                NumeroEmpleado = pedidoAClonar.RegistradoPor.NumeroEmpleado,
                IdTipoPedido = pedidoAClonar.Tipo.Id,
                IdMesa = pedidoAClonar.Mesa.Id,
                Contiene = CloneDominioADatosContiene(pedidoAClonar.Contiene)

            };
        }

        public List<AccesoADatos.Contiene> CloneDominioADatosContiene(List<Contiene> listaDeSolicitud)
        {
            List<AccesoADatos.Contiene> listaRetorno = new List<AccesoADatos.Contiene>();

            foreach (Contiene solicitud in listaDeSolicitud)
            {
                listaRetorno.Add(new AccesoADatos.Contiene()
                {
                    CodigoBarra = solicitud.ArticuloVenta.CodigoBarra,
                    Cantidad = solicitud.Cantidad,
                    Total = solicitud.Total
                });
            }

            return listaRetorno;
        }

        public List<Pedido> ObtenerPedidos() 
        {
            List<Pedido> listaARetornar = new List<Pedido>();
            PedidosDAO dao = new PedidosDAO();
            List < AccesoADatos.Pedido > pedidosEncontrados = dao.ObtenerPedidos();
            
            foreach (AccesoADatos.Pedido pedido in pedidosEncontrados)
            {
                if (pedido.Mesa == null)
                {

                    listaARetornar.Add(Pedido.CloneParaLlevar(pedido));
                }
                else
                {
                    listaARetornar.Add(Pedido.CloneParaLocal(pedido));
                }
            }

            return listaARetornar;
        
        }

        public double ObtenerIngresosDelDia()
        {
            decimal sumatoria = 0;
            PedidosDAO pedidoDAO = new PedidosDAO();
            List<AccesoADatos.Pedido> listaDePedidos = new List<AccesoADatos.Pedido>();
            listaDePedidos = pedidoDAO.ObtenerPedidosDelDia();

            foreach (AccesoADatos.Pedido pedido in listaDePedidos)
            {
                sumatoria += pedido.Total;
            }

            double total = Convert.ToDouble(sumatoria);

            return total;
        }
        
        public List<Pedido> ObtenerPedidosHoy()
        {
            List<Pedido> pedidoList = new List<Pedido>();
            PedidosDAO pedidosDAO = new PedidosDAO();
            List<AccesoADatos.Pedido> pedidosEncontrados = pedidosDAO.ObtenerPedidosHoy();
            foreach (AccesoADatos.Pedido pedido in pedidosEncontrados)
            {
                if (pedido.Mesa == null)
                {

                    pedidoList.Add(Pedido.CloneParaLlevar(pedido));
                }
                else
                {
                    pedidoList.Add(Pedido.CloneParaLocal(pedido));
                }
            }
            return pedidoList;
        }


        public List<Pedido> ObtenerPedidosCliente(string pedidoBuscarNombre)
        {
            List<Pedido> listaARetornar = new List<Pedido>();
            PedidosDAO dao = new PedidosDAO();
           
            List<AccesoADatos.Pedido> pedidosEncontrados = dao.ObtenerPedidoPorNombre(pedidoBuscarNombre);

            foreach (AccesoADatos.Pedido pedido in pedidosEncontrados)
            {
                if (pedido.Mesa == null)
                {
                    
                    listaARetornar.Add(Pedido.CloneParaLlevar(pedido));
                }
                else
                {
                    listaARetornar.Add(Pedido.CloneParaLocal(pedido));
                }
            }
            return listaARetornar;
        }

        public List<Pedido> ObtenerPedidosPreparar()
        {
            List<Pedido> pedidoList = new List<Pedido>();
            PedidosDAO pedidosDAO = new PedidosDAO();
            List<AccesoADatos.Pedido> pedidosEncontrados = pedidosDAO.ObtenerPedidosEnPreparacion();

            foreach (AccesoADatos.Pedido pedido in pedidosEncontrados)
            {
                if (pedido.Mesa == null)
                {

                    pedidoList.Add(Pedido.CloneParaLlevar(pedido));
                }
                else
                {

                    pedidoList.Add(Pedido.CloneParaLocal(pedido));

                }
            }

            return pedidoList;
        }
    }
}
