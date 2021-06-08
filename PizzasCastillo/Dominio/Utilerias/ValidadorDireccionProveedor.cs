using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using FluentValidation.Results;

namespace Dominio.Utilerias
{
    public class ValidadorDireccionProveedor : AbstractValidator<DireccionProveedor>
    {
        public ValidadorDireccionProveedor()
        {
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(50).MinimumLength(5);
            RuleFor(x => x.Numero).NotEmpty().MaximumLength(5);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.CodigoPostal).NotNull().NotEmpty();
            RuleFor(x => x.EntidadFederativa).NotEmpty().NotNull().MaximumLength(20).MinimumLength(3);

        }

        public bool Validar(DireccionProveedor direccion)
        {
            var validator = new ValidadorDireccionProveedor();
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
