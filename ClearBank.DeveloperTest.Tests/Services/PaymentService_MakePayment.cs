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
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IPaymentRequestValidatorFactory _paymentRequestValidatorFactory;
        private readonly IDataStore _dataStore;
        private readonly IPaymentRequestValidator _paymentRequestValidator;
        private readonly PaymentService _sut;

        public PaymentService_MakePayment()
        {
            _dataStore = Substitute.For<IDataStore>();
            _paymentRequestValidator = Substitute.For<IPaymentRequestValidator>();

            _dataStoreFactory = Substitute.For<IDataStoreFactory>();
            _paymentRequestValidatorFactory = Substitute.For<IPaymentRequestValidatorFactory>();

            _dataStoreFactory.Get().Returns(_dataStore);
            _paymentRequestValidatorFactory.Get(Arg.Any<PaymentScheme>()).Returns(_paymentRequestValidator);
            
            _sut = new PaymentService(_dataStoreFactory, _paymentRequestValidatorFactory);
        }

        [Fact]
        public void Should_Attempt_Account_Retrieval_From_DataStore()
        {
            var accountNumber = "ValidAccountNumber";

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _dataStore.Received(1).GetAccount(Arg.Is<string>(x => x == accountNumber));
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
        public void Should_Update_Account_When_Payment_Request_Passes_Validation()
        {
            var accountNumber = "ValidAccountNumber";
            _dataStore.GetAccount(Arg.Any<string>()).Returns(new Account());
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(true);

            _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            _dataStore.Received(1).UpdateAccount(Arg.Any<Account>());
        }

        [Fact]
        public void Should_Return_Success_Result_When_Payment_Request_Validation_Passes()
        {
            var accountNumber = "ValidAccountNumber";
            _dataStore.GetAccount(Arg.Any<string>()).Returns(new Account());
            _paymentRequestValidator.IsValid(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>()).Returns(true);

            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = accountNumber });

            result.Success.Should().BeTrue();
        }
    }
}