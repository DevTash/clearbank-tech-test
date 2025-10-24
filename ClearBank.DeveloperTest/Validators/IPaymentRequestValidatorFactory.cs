using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentRequestValidatorFactory
    {
        IPaymentRequestValidator? Get(PaymentScheme paymentScheme);
    }
}
