using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentRequestValidator
    {
        bool IsValid(Account account, MakePaymentRequest request);
    }
}
