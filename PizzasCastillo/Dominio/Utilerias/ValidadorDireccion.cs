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
    public class ValidadorDireccion : AbstractValidator<Direccion>
    {
        public ValidadorDireccion()
        {
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(30).MinimumLength(5);
            RuleFor(x => x.Colonia).NotEmpty().MaximumLength(30).MinimumLength(4);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.CodigoPostal).NotNull().NotEmpty();
            RuleFor(x => x.Referencias).NotEmpty().NotNull().MaximumLength(400).MinimumLength(5);
            RuleFor(x => x.NumeroExterior).NotEmpty().NotNull();
            RuleFor(x => x.EntidadFederativa).NotEmpty().NotNull().MaximumLength(20).MinimumLength(3);
        }

        public bool Validar(Direccion direccion)
        {
            var validator = new ValidadorDireccion();
            ValidationResult result = validator.Validate(direccion);

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
