using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IPaymentRequestValidatorFactory _paymentRequestValidatorFactory;

        public PaymentService(
            IDataStoreFactory dataStoreFactory,
            IPaymentRequestValidatorFactory paymentRequestValidatorFactory
        )
        {
            _dataStoreFactory = dataStoreFactory;
            _paymentRequestValidatorFactory = paymentRequestValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _dataStoreFactory.Get().GetAccount(request.DebtorAccountNumber);
            var validator = _paymentRequestValidatorFactory.Get(request.PaymentScheme);

            var result = new MakePaymentResult();
            result.Success = validator?.IsValid(account, request) ?? false;
            
            if (result.Success)
            {
                account.Balance -= request.Amount;
                _dataStoreFactory.Get().UpdateAccount(account);
            }

            return result;
        }
    }
}
