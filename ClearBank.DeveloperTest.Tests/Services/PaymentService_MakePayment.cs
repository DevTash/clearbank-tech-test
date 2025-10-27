using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NSubstitute;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentService_MakePayment
    {
        private readonly IPaymentRequestValidatorFactory _paymentRequestValidatorFactory;
        private readonly IAccountService _accountService;
        private readonly IPaymentRequestValidator _paymentRequestValidator;
        private readonly PaymentService _sut;

        public PaymentService_MakePayment()
        {
            _accountService = Substitute.For<IAccountService>();
            _paymentRequestValidator = Substitute.For<IPaymentRequestValidator>();
            _paymentRequestValidatorFactory = Substitute.For<IPaymentRequestValidatorFactory>();

            _paymentRequestValidatorFactory.Get(Arg.Any<PaymentScheme>()).Returns(_paymentRequestValidator);
            
            _sut = new PaymentService(_accountService, _paymentRequestValidatorFactory);
        }

        [Fact]
        public void Should_Retrieve_Account()
        {
            var accountNumber = "ValidAccountNumber";

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _accountService.Received(1).GetAccount(Arg.Is<string>(x => x == accountNumber));
        }

        [Fact]
        public void Should_Validate_Payment_Request()
        {
            var accountNumber = "ValidAccountNumber";

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _paymentRequestValidator.Received(1).IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>());
        }

        [Fact]
        public void Should_Return_Not_Success_Result_When_Payment_Request_Fails_Validation()
        {
            var accountNumber = "ValidAccountNumber";
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(false);

            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Debit_Account_When_Payment_Request_Fails_Validation()
        {
            var accountNumber = "ValidAccountNumber";
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(false);

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _accountService.DidNotReceive().DebitAccount(Arg.Any<Account>(), Arg.Any<decimal>());
        }

        [Fact]
        public void Should_Debit_Account_When_Payment_Request_Passes_Validation()
        {
            var accountNumber = "ValidAccountNumber";
            _accountService.GetAccount(Arg.Any<string>()).Returns(new Account());
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(true);

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _accountService.Received(1).DebitAccount(Arg.Any<Account>(), Arg.Any<decimal>());
        }

        [Fact]
        public void Should_Debit_Account_When_There_Is_No_Validator()
        {
            var accountNumber = "ValidAccountNumber";
            _accountService.GetAccount(Arg.Any<string>()).Returns(new Account());
            _paymentRequestValidatorFactory.Get(Arg.Any<PaymentScheme>()).Returns((IPaymentRequestValidator)null);

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _accountService.Received(1).DebitAccount(Arg.Any<Account>(), Arg.Any<decimal>());
        }

        [Fact]
        public void Should_Return_Success_Result_When_There_Is_No_Validator()
        {
            var accountNumber = "ValidAccountNumber";
            _accountService.GetAccount(Arg.Any<string>()).Returns(new Account());           
            _paymentRequestValidatorFactory.Get(Arg.Any<PaymentScheme>()).Returns((IPaymentRequestValidator) null);

            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            result.Success.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_Success_Result_When_Payment_Request_Validation_Passes()
        {
            var accountNumber = "ValidAccountNumber";
            _accountService.GetAccount(Arg.Any<string>()).Returns(new Account());
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(true);

            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            result.Success.Should().BeTrue();
        }
    }
}