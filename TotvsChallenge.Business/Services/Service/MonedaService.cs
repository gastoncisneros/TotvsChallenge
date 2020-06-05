using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public VueltoDTO Pagar(PagoDTO pago)
        {
            VueltoDTO vuelto = new VueltoDTO();
            PagoValidator validationRules = new PagoValidator(_monedasOptions);

            ValidateGeneral(validationRules, pago, vuelto);
            if (vuelto.Errors.Count() > 0)
                return vuelto;

            int[] monedas = _monedasOptions.Value.Monedas;
            Array.Sort(monedas);

            var montoVuelto = pago.CantidadPagada - pago.CantidadAPagar;

            GuardarTransaccion(pago, montoVuelto);

            vuelto.Vuelto = DarVuelto(monedas, montoVuelto);

            vuelto.IsSuccess = true;
            return vuelto;
        }

        public void ValidateGeneral(PagoValidator validator, PagoDTO pago, VueltoDTO vuelto)
        {
            var errors = validator.Validate(pago);
            vuelto.Errors = errors.Errors.Select(x => new ErrorDS
            {
                Descr = x.ErrorMessage,
                ParentKey = x.ErrorCode,
                Key = x.PropertyName
            }).ToArray();
        }

        private string DarVuelto(int[] monedas, int monto)
        {
            int i = 0;
            int solucionInt = 0;
            string solucion = "";
            List<int> numbers = new List<int>();

            while (solucionInt != monto)
            {
                i = monedas.Length - 1;
                while (i >= 0)
                {
                    if ((solucionInt + monedas[i]) <= monto)
                    {
                        solucionInt = solucionInt + monedas[i];
                        numbers.Add(monedas[i]);
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }

            var cantidadMonedas = numbers.GroupBy(x => x);
            foreach (var key in cantidadMonedas) 
            {
                var coin = key.Count() > 1 ? " monedas de " : " moneda de ";
                solucion += key.Count() + coin + key.Key.ToString() + ", ";
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
