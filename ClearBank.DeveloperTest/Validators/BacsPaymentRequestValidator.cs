using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class BacsPaymentRequestValidator : BasePaymentRequestValidator
    {
        public BacsPaymentRequestValidator() : base(AllowedPaymentSchemes.Bacs)
        {
        }
    }
}
