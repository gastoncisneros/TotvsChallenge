using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Business.Services.Interface;
using TotvsChallenge.Domain.DTO;

namespace TotvsChallenge.Business.Services.Service
{
    public class MonedaService : IMonedaService
    {
        public string Pagar(PagoDTO pago)
        {
            int[] monedas = {1,2,5,10, 20, 50, 100 };
            var montoVuelto = pago.CantidadPagada - pago.CantidadAPagar;
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
    }
}
