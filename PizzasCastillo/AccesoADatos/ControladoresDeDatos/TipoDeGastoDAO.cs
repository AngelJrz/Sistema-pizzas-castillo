using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ControladoresDeDatos
{
    public class TipoDeGastoDAO
    {
        private PizzasBDEntities conexion;
        private List<TipoGasto> tiposDeGasto;

        public TipoDeGastoDAO()
        {
            conexion = new PizzasBDEntities();
            tiposDeGasto = null;
        }

        public List<TipoGasto> ObtenerTiposDeGastos()
        {
            tiposDeGasto = conexion.TipoGasto.ToList();

            return tiposDeGasto;
        }

        public bool ModificarTipoDeGasto(int id, string nuevoNombre)
        {
            bool modificado = false;

            try
            {
                TipoGasto tipoGastoExistente = conexion.TipoGasto.FirstOrDefault(tipo => tipo.Nombre == nuevoNombre);

                if (tipoGastoExistente == null)
                {
                    TipoGasto tipoGasto = conexion.TipoGasto.FirstOrDefault(tipo => tipo.Id == id);
                    tipoGasto.Nombre = nuevoNombre;
                    conexion.Entry(tipoGasto).State = EntityState.Modified;
                    conexion.SaveChangesAsync();
                    modificado = true;
                }
            }
            catch (Exception)
            {
                return modificado;
            }

            return modificado;
        }

        public bool RegistrarTipoDeGasto(TipoGasto nuevoGasto)
        {
            bool registrado = false;

            try
            {
                TipoGasto tipoGastoExistente = conexion.TipoGasto.FirstOrDefault(tipo => tipo.Nombre == nuevoGasto.Nombre);

                if (tipoGastoExistente == null)
                {
                    conexion.Entry(nuevoGasto).State = EntityState.Added;
                    conexion.SaveChanges();
                    registrado = true;
                }
            }
            catch (DbUpdateException)
            {
                return registrado;
            }

            return registrado;
        }
    }
}
