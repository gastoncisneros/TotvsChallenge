using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using TotvsChallenge.Business.DataValidation;
using TotvsChallenge.Domain.DTO;
using TotvsChallenge.Domain.Options;

namespace Tests
{
    public class Tests
    {
        [TestFixture]
        public class PagoValidatorTest
        {
            private PagoValidator validator;

            int[] monedas = { 10, 20, 50, 100 };
            

            [SetUp]
            public void Setup()
            {
                var monedasOptions = new MonedasOptions { Monedas = monedas };
                var mockOptions = new Mock<IOptions<MonedasOptions>>();
                mockOptions.Setup(ap => ap.Value).Returns(monedasOptions);
                
                validator = new PagoValidator(mockOptions.Object);
            }
            
            [Test]
            public void Error_Cuando_CantidadPagada_Es_Negativo()
            {
                PagoDTO pago = new PagoDTO { CantidadAPagar = 100, CantidadPagada = -1 };

                var error = validator.TestValidate(pago);

                error.ShouldHaveValidationErrorFor(x => x.CantidadPagada)
                    .WithErrorMessage("La CantidadPagada debe ser mayor o igual a 0");
            }
            

            [Test]
            public void Error_Cuando_CantidadPagada_EsMenorA_CantidadAPagar()
            {
                PagoDTO pago = new PagoDTO { CantidadAPagar = 100, CantidadPagada = 80 };

                var error = validator.TestValidate(pago);

                error.ShouldHaveValidationErrorFor(x => x.CantidadPagada)
                    .WithErrorMessage("La CantidadPagada debe ser mayor o igual a la CantidadAPagar");
            }


            [Test]
            public void Error_Por_Monedas()
            {
                PagoDTO pago = new PagoDTO { CantidadAPagar = 100, CantidadPagada = 145 };

                var error = validator.TestValidate(pago);

                error.ShouldHaveValidationErrorFor(x => x.CantidadPagada)
                    .WithErrorMessage("No posee las monedas con denominacion necesaria para este vuelto. Por favor revise sus monedas");
            }


            [Test]
            public void Error_Cuando_CantidadAPagar_Es_Negativo()
            {
                PagoDTO pago = new PagoDTO { CantidadAPagar = -1, CantidadPagada = 0 };

                var error = validator.TestValidate(pago);

                error.ShouldHaveValidationErrorFor(x => x.CantidadAPagar)
                    .WithErrorMessage("La CantidadAPagar debe ser mayor o igual a 0");
            }
            
        }
    }
}