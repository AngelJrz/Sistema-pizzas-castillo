using Dominio.Enumeraciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilerias
{
    public class ValidadorTipoUsuario : AbstractValidator<Tipo>
    {
        public ValidadorTipoUsuario()
        {
            RuleFor(tipo => tipo.Nombre).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleFor(tipo => tipo.Estatus).InclusiveBetween(0, 1);
        }

        public bool Validar(Tipo tipo)
        {
            var validator = new ValidadorTipoUsuario();
            ValidationResult result = validator.Validate(tipo);

            if (!result.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
