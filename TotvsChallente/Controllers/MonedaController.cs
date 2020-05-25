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
        public JsonResult Pagar([FromBody] PagoDTO pago)
        {
            var result = _monedaService.Pagar(pago);
            return Json(result);
            
        }
        

    }
}
