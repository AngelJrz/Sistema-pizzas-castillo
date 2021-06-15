using Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Dominio.Utilerias
{
    public class ValidadorReporta : AbstractValidator<Reporta>
    {
        private ValidationResult resultado;
        public ValidadorReporta()
        {
            RuleFor(x => x.Producto.CodigoBarra).NotNull().NotEmpty();
            RuleFor(x => x.CantidadEnInventario).NotNull().NotEmpty();
            RuleFor(x => x.Comentario).MaximumLength(255);
        }

        public bool Validar(Reporta producto)
        {
            var validador = new ValidadorReporta();
            resultado = validador.Validate(producto);

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
