using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotvsChallenge.Domain.DTO;
using TotvsChallenge.Domain.Options;

namespace TotvsChallenge.Business.DataValidation
{
    public class PagoValidator : AbstractValidator<PagoDTO>
    {
        private readonly IOptions<MonedasOptions> _monedasOptions;
        public PagoValidator(IOptions<MonedasOptions> monedasOptions)
        {
            _monedasOptions = monedasOptions;
            
            RuleFor(m => m.CantidadPagada).NotNull().WithMessage("La CantidadPagada es requerida")
           .GreaterThanOrEqualTo(0).WithMessage("La CantidadPagada debe ser mayor o igual a 0")
           .GreaterThanOrEqualTo(x => x.CantidadAPagar).WithMessage("La CantidadPagada debe ser mayor o igual a la CantidadAPagar");

            RuleFor(m => m.CantidadPagada).Must(x => EsDivisble(x, _monedasOptions.Value.Monedas.Min()))
                .WithMessage("No posee las monedas con denominacion necesaria para este vuelto. Por favor revise sus monedas");

            RuleFor(m => m.CantidadAPagar).NotNull().WithMessage("La CantidadAPagar es requerida")
           .GreaterThanOrEqualTo(0).WithMessage("La CantidadAPagar debe ser mayor o igual a 0");

        }
        

        public bool EsDivisble(int x, int n)
        {
            x = x % 10;
            return (x % n) == 0;
        }
    }
}
