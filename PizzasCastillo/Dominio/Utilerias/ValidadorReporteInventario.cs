using FluentValidation.Results;
using System.Collections.Generic;
using Dominio.Entidades;
using FluentValidation;
namespace Dominio.Utilerias
{
    public class ValidadorReporteInventario : AbstractValidator<ReporteInventario>
    {
        private ValidationResult resultado;
        public ValidadorReporteInventario()
        {
            RuleFor(x => x.Fecha).NotNull();
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(60);
            RuleFor(x => x.GeneradoPor.Id).NotNull().NotEmpty();
            RuleFor(x => x.Reporta.Count).NotNull().NotEmpty().NotEqual(0);
        }

        public bool Validar(ReporteInventario reporte)
        {
            var validador = new ValidadorReporteInventario();
            resultado = validador.Validate(reporte);

            if (!resultado.IsValid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<string> ObtenerPropiedadesIncorrectas()
        {
            List<string> propiedadesIncorrectas = new List<string>();

            if (resultado != null)
            {
                foreach (var error in resultado.Errors)
                {
                    propiedadesIncorrectas.Add(error.PropertyName);
                }
            }

            return propiedadesIncorrectas;
        }
    }
}
