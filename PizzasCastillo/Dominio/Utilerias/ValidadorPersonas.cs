using Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilerias
{
    public class ValidadorPersonas : AbstractValidator<Persona>
    {
        public ValidadorPersonas()
        {
            RuleFor(x => x.Nombres).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Telefono).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(x => x.Direcciones.Count).NotEmpty().NotNull().NotEqual(0);
        }

        public bool Validar(Persona persona)
        {
            var validator = new ValidadorPersonas();
            ValidationResult result = validator.Validate(persona);

            if (!result.IsValid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
