using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class PaymentRequestValidatorFactory : IPaymentRequestValidatorFactory
    {
        public IPaymentRequestValidator Get(PaymentScheme paymentScheme)
        {
            return paymentScheme switch
            {
                PaymentScheme.Bacs => (IPaymentRequestValidator) new BacsPaymentRequestValidator(),
                PaymentScheme.FasterPayments => (IPaymentRequestValidator) new FasterPaymentsPaymentRequestValidator(),
                PaymentScheme.Chaps => (IPaymentRequestValidator) new ChapsPaymentRequestValidator(),
                _ => null
            };
        }
    }
}