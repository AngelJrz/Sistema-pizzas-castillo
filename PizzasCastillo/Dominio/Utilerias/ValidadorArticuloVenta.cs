﻿using Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilerias
{
    public class ValidadorArticuloVenta : AbstractValidator<ArticuloVenta>
    {
        public ValidadorArticuloVenta()
        {
            RuleFor(x => x.Foto).NotEmpty().NotNull();
            RuleFor(x => x.Precio).NotEmpty().NotNull();
            RuleFor(x => x.NombreFoto).NotEmpty().NotNull();
            RuleFor(x => x.Nombre).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.Platillo.Receta).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Platillo.Consume).NotNull().NotEmpty();
        }

        public bool Validar(ArticuloVenta articuloVenta)
        {
            var validator = new ValidadorArticuloVenta();
            ValidationResult result = validator.Validate(articuloVenta);

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
