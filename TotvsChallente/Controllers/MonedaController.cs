using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TotvsChallenge.Business.Services.Interface;
using TotvsChallenge.Domain.DTO;

namespace TotvsChallente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpPost]
        public string Pagar([FromBody] PagoDTO pago)
            => _monedaService.Pagar(pago);
        

    }
}
