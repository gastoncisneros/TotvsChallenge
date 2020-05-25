using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Domain.DTO;

namespace TotvsChallenge.Business.DataValidation
{
    public class PagoValidator : AbstractValidator<PagoDTO>
    {
        public PagoValidator()
        {
            RuleFor(m => m.CantidadPagada).NotEqual(0).WithMessage("La cantidad pagada no puede ser 0");
            RuleFor(m => m.CantidadPagada).NotEqual(145).WithMessage("La cantidad pagada no puede ser 145");
        }
    }
}
