using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Business.DataValidation;
using TotvsChallenge.Business.Services.Interface;
using TotvsChallenge.DataAccess.Repository.Interface;
using TotvsChallenge.Domain;
using TotvsChallenge.Domain.DTO;
using TotvsChallenge.Domain.Options;

namespace TotvsChallenge.Business.Services.Service
{
    public class MonedaService : IMonedaService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IOptions<MonedasOptions> _monedasOptions;

        public MonedaService(ITransaccionRepository transaccionRepository, IOptions<MonedasOptions> monedasOptions)
        {
            _transaccionRepository = transaccionRepository;
            _monedasOptions = monedasOptions;
        }

        public string Pagar(PagoDTO pago)
        {
            int[] monedas = _monedasOptions.Value.Monedas;

            var montoVuelto = pago.CantidadPagada - pago.CantidadAPagar;

            GuardarTransaccion(pago, montoVuelto);

            string cantidadDeMonedas = DarVuelto(monedas, montoVuelto);

            return cantidadDeMonedas;
        }

        private string DarVuelto(int[] monedas, int monto)
        {
            int i = 0;
            int solucionInt = 0;
            string solucion = "";

            while (solucionInt != monto)
            {
                i = monedas.Length - 1;
                while (i >= 0)
                {
                    if ((solucionInt + monedas[i]) <= monto)
                    {
                        solucionInt = solucionInt + monedas[i];
                        solucion += " Una moneda de : " + monedas[i];
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }

            return solucion;
        }

        private void GuardarTransaccion(PagoDTO pago, int montoVuelto)
        {
            var transaccion = new Transaccion
            {
                MontoPagado = pago.CantidadPagada,
                MontoDevuelto = montoVuelto
            };

            _transaccionRepository.GuardarTransaccion(transaccion);
        }
    }
}
