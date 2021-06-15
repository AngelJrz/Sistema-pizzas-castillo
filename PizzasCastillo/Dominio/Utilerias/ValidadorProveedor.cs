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
    public class ValidadorProveedor : AbstractValidator<Proveedor>
    {
        public ValidadorProveedor()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50).NotNull();
            RuleFor(x => x.NombreEncargado).NotEmpty().MaximumLength(50).NotNull();
            RuleFor(x => x.Telefono).NotEmpty().MaximumLength(11).NotNull();
            RuleFor(x => x.Email).NotEmpty().MaximumLength(50).NotNull().EmailAddress();
            RuleFor(x => x.Dni).NotEmpty().MaximumLength(10).NotNull();
        }

        public bool Validar(Proveedor proveedor)
        {
            var validator = new ValidadorProveedor();
            ValidationResult result = validator.Validate(proveedor);

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
