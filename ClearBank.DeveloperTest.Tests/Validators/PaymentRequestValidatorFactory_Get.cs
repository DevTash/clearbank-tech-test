using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class PaymentRequestValidatorFactory_Get
    {
        private readonly PaymentRequestValidatorFactory _sut;

        public PaymentRequestValidatorFactory_Get()
        {
            _sut = new PaymentRequestValidatorFactory();
        }

        [Fact]
        public void Should_Return_Bacs_Payment_Validator_When_Payment_Scheme_Is_Bacs()
        {
            _sut.Get(Types.PaymentScheme.Bacs)
                .Should()
                .BeOfType<BacsPaymentRequestValidator>();
        }

        [Fact]
        public void Should_Return_FasterPayments_Payment_Validator_When_Payment_Scheme_Is_FasterPayments()
        {
            _sut.Get(PaymentScheme.FasterPayments)
                .Should()
                .BeOfType<FasterPaymentsPaymentRequestValidator>();
        }

        [Fact]
        public void Should_Return_Chaps_Payment_Validator_When_Payment_Scheme_Is_Chaps()
        {
            _sut.Get(PaymentScheme.Chaps)
                .Should()
                .BeOfType<ChapsPaymentRequestValidator>();
        }

        [Fact]
        public void Should_Throw_When_PaymentScheme_Is_Not_Supported()
        {
            Action act = () => _sut.Get((PaymentScheme) 999);

            act.Should()
                .Throw<NotSupportedException>()
                .WithMessage("Payment scheme 999 is not supported");
        }
    }
}