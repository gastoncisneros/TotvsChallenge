using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Domain.DTO;

namespace TotvsChallenge.Business.Services.Interface
{
    public interface IMonedaService
    {
        VueltoDTO Pagar(PagoDTO pago);
    }
}
