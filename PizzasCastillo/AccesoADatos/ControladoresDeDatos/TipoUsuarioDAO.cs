using AccesoADatos.Excepciones;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class TipoUsuarioDAO
    {
        private readonly PizzasBDEntities conexion;
        private List<TipoUsuario> tiposUsuario;
        private int _resultado;
        private const int NINGUN_CAMBIO_REALIZADO = 0;
        private const int NO_ACTIVO = 0;

        public TipoUsuarioDAO()
        {
            conexion = new PizzasBDEntities();
            _resultado = 0;
        }

        public List<TipoUsuario> ObtenerTiposUsuario()
        {
            try
            {
                tiposUsuario = conexion.TipoUsuario.ToList();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            

            return tiposUsuario;
        }

        public int ObtenerNumeroDeTipoUsuario(string nombreTipoUsuario)
        {
            return conexion.Persona.Where(empleado => empleado.TipoUsuario.Nombre.Equals(nombreTipoUsuario))
                .ToList().Count;
        }

        public bool AgregarNuevoTipoUsuario(TipoUsuario tipoUsuario)
        {
            conexion.Entry(tipoUsuario).State = EntityState.Added;

            try
            {
                _resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ModificacionDBFallidaException(ex);
            }
            catch(EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
                return false;

            return true;
        }

        public bool DarDeBajaTipoUsuario(TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoUsuarioADarDeBaja;
            try
            {
                tipoUsuarioADarDeBaja = conexion.TipoUsuario.SingleOrDefault(tipo =>
                tipo.Id == tipoUsuario.Id || tipo.Nombre.Equals(tipoUsuario.Nombre));
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            

            if (tipoUsuarioADarDeBaja == null)
                return false;

            tipoUsuarioADarDeBaja.Estatus = NO_ACTIVO;

            conexion.Entry(tipoUsuarioADarDeBaja).State = EntityState.Modified;

            try
            {
                _resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ModificacionDBFallidaException(ex);
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
                return false;

            return true;
        }

        public bool EditarTipoUsuario(TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoUsuarioAEditar;
            try
            {
                tipoUsuarioAEditar = conexion.TipoUsuario.SingleOrDefault(tipo =>
                tipo.Id == tipoUsuario.Id || tipo.Nombre.Equals(tipoUsuario.Nombre));
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }


            if (tipoUsuarioAEditar == null)
                return false;

            tipoUsuarioAEditar.Nombre = tipoUsuario.Nombre;
            tipoUsuarioAEditar.Estatus = tipoUsuario.Estatus;

            conexion.Entry(tipoUsuarioAEditar).State = EntityState.Modified;

            try
            {
                _resultado = conexion.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ModificacionDBFallidaException(ex);
            }
            catch (EntityException ex)
            {
                throw new ConexionFallidaException(ex);
            }

            if (_resultado == NINGUN_CAMBIO_REALIZADO)
                return false;

            return true;
        }
    }
}
