using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class BacsPaymentRequestValidator : BasePaymentRequestValidator, IPaymentRequestValidator
    {
        public BacsPaymentRequestValidator() : base(AllowedPaymentSchemes.Bacs)
        {
        }
    }
}
