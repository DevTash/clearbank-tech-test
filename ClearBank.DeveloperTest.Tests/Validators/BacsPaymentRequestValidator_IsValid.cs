using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BacsPaymentRequestValidator_IsValid
    {
        private readonly BacsPaymentRequestValidator _sut;

        public BacsPaymentRequestValidator_IsValid()
        {
            _sut = new BacsPaymentRequestValidator();
        }

        [Fact]
        public void Should_Return_False_When_Account_Is_Null()
        {
            var result = _sut.IsValid(null, null);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_False_When_Account_Does_Not_Allow_Bacs_Payments()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };

            var result =  _sut.IsValid(account, null);

            result.Should().BeFalse();  
        }

        [Fact]
        public void Should_Return_True_When_Account_Is_In_Valid_State()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            var result = _sut.IsValid(account, null);

            result.Should().BeTrue();
        }
    }
}