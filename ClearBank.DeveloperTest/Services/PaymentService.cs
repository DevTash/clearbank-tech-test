using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentRequestValidatorFactory _paymentRequestValidatorFactory;

        public PaymentService(
            IAccountService accountService,
            IPaymentRequestValidatorFactory paymentRequestValidatorFactory
        )
        {
            _accountService = accountService;
            _paymentRequestValidatorFactory = paymentRequestValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);    
            var validator = _paymentRequestValidatorFactory.Get(request.PaymentScheme);

            var result = new MakePaymentResult();
            result.Success = validator?.IsValid(account, request) ?? true;
            
            if (result.Success)
            {
                _accountService.DebitAccount(account, request.Amount);
            }

            return result;
        }
    }
}
