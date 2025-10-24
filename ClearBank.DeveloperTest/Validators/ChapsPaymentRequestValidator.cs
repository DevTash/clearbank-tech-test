using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class ChapsPaymentRequestValidator : BasePaymentRequestValidator
    {
        public ChapsPaymentRequestValidator() : base(AllowedPaymentSchemes.Chaps)
        {
        }

        public override bool IsValid(Account account, MakePaymentRequest request)
        {
            if (base.IsValid(account, request) == false)
            {
                return false;
            }

            if (account.Status != AccountStatus.Live)
            {
                return false;
            }

            return true;
        }
    }
}