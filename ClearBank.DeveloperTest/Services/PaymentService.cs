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
            var dataStore = _dataStoreFactory.Get();
            var account = dataStore.GetAccount(request.DebtorAccountNumber);
            var validator = _paymentRequestValidatorFactory.Get(request.PaymentScheme);

            var result = new MakePaymentResult();
            result.Success = validator?.IsValid(account, request) ?? true;
            
            if (result.Success)
            {
                account.Balance -= request.Amount;
                dataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
