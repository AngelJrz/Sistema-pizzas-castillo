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
        private ValidationResult result;
        public ValidadorDireccion()
        {
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(100).MinimumLength(8);
            RuleFor(x => x.Colonia).NotEmpty().MaximumLength(100).MinimumLength(4);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(50).MinimumLength(4);
            RuleFor(x => x.CodigoPostal).NotNull().NotEmpty().MaximumLength(6).MinimumLength(5).WithName("C.P.");
            RuleFor(x => x.Referencias).NotEmpty().NotNull().MaximumLength(200).MinimumLength(5);
            RuleFor(x => x.NumeroExterior).NotEmpty().NotNull().MaximumLength(4).WithName("Numero exterior");
            RuleFor(x => x.EntidadFederativa).NotEmpty().NotNull().MaximumLength(20).MinimumLength(3).WithName("Estado");
        }

        public bool Validar(Direccion direccion)
        {
            var validator = new ValidadorDireccion();
            result = validator.Validate(direccion);

            if (!result.IsValid)
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

            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    propiedadesIncorrectas.Add(error.PropertyName);
                }
            }

            return propiedadesIncorrectas;
        }
    }
}
