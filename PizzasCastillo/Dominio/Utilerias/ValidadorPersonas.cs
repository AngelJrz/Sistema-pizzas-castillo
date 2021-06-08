using Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio.Utilerias
{
    public class ValidadorPersonas : AbstractValidator<Persona>
    {
        public ValidadorPersonas()
        {
            RuleFor(x => x.Nombres).NotEmpty().MinimumLength(2).MaximumLength(150).NotNull();
            RuleFor(x => x.Apellidos).NotEmpty().MinimumLength(2).MaximumLength(150).NotNull();
            RuleFor(x => x.Email).NotEmpty().MinimumLength(10).MaximumLength(80).NotNull().EmailAddress();
            RuleFor(x => x.Telefono).NotNull().NotEmpty().MinimumLength(10).MaximumLength(10).Must(EsNumero);
            RuleFor(x => x.Direcciones.Count).NotEmpty().NotNull().NotEqual(0);
        }

        private bool EsNumero(string numero)
        {
            Regex numeroExpresionRegular = new Regex(@"\d");

            return numeroExpresionRegular.IsMatch(numero);
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
