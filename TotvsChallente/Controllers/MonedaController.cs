using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TotvsChallenge.Business.DataValidation;
using TotvsChallenge.Business.Services.Interface;
using TotvsChallenge.Domain.DTO;

namespace TotvsChallente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : Controller
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpPost]
        public IActionResult Pagar([FromBody] PagoDTO pago)
        {
            PagoValidator validationRules = new PagoValidator();
            var errors = validationRules.Validate(pago);
            if (errors.Errors.Count > 0)
            {
                return Json(errors.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
                var result = _monedaService.Pagar(pago);
                if (!result.Any())
                {
                    return NotFound(pago);
                }
                return Ok(result);
            }
        }
        

    }
}
