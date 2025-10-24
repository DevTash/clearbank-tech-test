using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public abstract class BasePaymentRequestValidator
    {
        private readonly AllowedPaymentSchemes _paymentScheme;

        public BasePaymentRequestValidator(AllowedPaymentSchemes paymentScheme)
        {
            _paymentScheme = paymentScheme;
        }

        public virtual bool IsValid(Account account, MakePaymentRequest request)
        {
            if (account == null)
            {
                return false;
            }

            if (!account.AllowedPaymentSchemes.HasFlag(_paymentScheme))
            {
                return false;
            }

            return true;
        }
    }
}