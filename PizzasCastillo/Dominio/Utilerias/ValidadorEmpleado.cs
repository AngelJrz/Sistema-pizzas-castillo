using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Dominio.Entidades;

namespace Dominio.Utilerias
{
    public class ValidadorEmpleado : AbstractValidator<Empleado>
    {
        public ValidadorEmpleado()
        {
            RuleFor(empleado => empleado.Username).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(empleado => empleado.Contrasenia).NotNull().NotEmpty();
            RuleFor(empleado => empleado.SalarioQuincenal).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(empleado => empleado.TipoUsuario).NotNull().NotEmpty();
        }

        public bool Validar(Empleado empleado)
        {
            var validator = new ValidadorEmpleado();
            ValidationResult result = validator.Validate(empleado);

            if (!result.IsValid)
            {
                return false;
            }
                
            return true;
        }
    }
}
