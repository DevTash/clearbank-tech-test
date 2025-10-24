using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class FasterPaymentsPaymentRequestValidator_IsValid
    {
        private readonly FasterPaymentsPaymentRequestValidator _sut;

        public FasterPaymentsPaymentRequestValidator_IsValid()
        {
            _sut = new FasterPaymentsPaymentRequestValidator();
        }

        [Fact]
        public void Should_Return_False_When_Account_Is_Null()
        {
            var result = _sut.IsValid(null, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_False_When_Account_Does_Not_Allow_Faster_Payments()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            var result = _sut.IsValid(account, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_False_When_Account_Balance_Is_Less_Than_Requested_Amount()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 300m
            };
            var request = new MakePaymentRequest
            {
                Amount = 500m
            };

            var result = _sut.IsValid(account, request);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_True_When_Account_Is_In_Valid_State()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 1000m
            };
            var request = new MakePaymentRequest
            {
                Amount = 500m
            };

            var result = _sut.IsValid(account, request);

            result.Should().BeTrue();
        }
    }
}