using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class FasterPaymentsPaymentRequestValidator : BasePaymentRequestValidator, IPaymentRequestValidator
    {
        public FasterPaymentsPaymentRequestValidator() : base(AllowedPaymentSchemes.FasterPayments)
        {
        }

        public override bool IsValid(Account account, MakePaymentRequest request)
        {
            if (base.IsValid(account, request) == false)
            {
                return false;
            }

            if (account.Balance < request.Amount)
            {
                return false;
            }

            return true;
        }
    }
}