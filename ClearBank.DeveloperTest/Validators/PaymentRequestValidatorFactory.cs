using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class PaymentRequestValidatorFactory : IPaymentRequestValidatorFactory
    {
        public IPaymentRequestValidator? Get(PaymentScheme paymentScheme)
        {
            return paymentScheme switch
            {
                PaymentScheme.Bacs => new BacsPaymentRequestValidator(),
                PaymentScheme.FasterPayments => new FasterPaymentsPaymentRequestValidator(),
                PaymentScheme.Chaps => new ChapsPaymentRequestValidator(),
                _ => null
            };
        }
    }
}