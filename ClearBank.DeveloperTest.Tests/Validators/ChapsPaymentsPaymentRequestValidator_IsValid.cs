using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class ChapsPaymentRequestValidator_IsValid
    {
        private readonly ChapsPaymentRequestValidator _sut;

        public ChapsPaymentRequestValidator_IsValid()
        {
            _sut = new ChapsPaymentRequestValidator();
        }

        [Fact]
        public void Should_Return_False_When_Account_Is_Null()
        {
            var result = _sut.IsValid(null, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_False_When_Account_Does_Not_Allow_Chaps_Payments()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            var result = _sut.IsValid(account, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_False_When_Account_Status_Is_Not_Live()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status =  AccountStatus.Disabled
            };

            var result = _sut.IsValid(account, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_True_When_Account_Is_In_Valid_State()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            var result = _sut.IsValid(account, null);

            result.Should().BeTrue();
        }
    }
}