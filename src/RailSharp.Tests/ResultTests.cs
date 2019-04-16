using FluentAssertions;
using RailSharp.Internal.Result;
using Xunit;

namespace RailSharp.Tests
{
    public class ResultTests
    {
        [Fact]
        public static void Failure_ReturnsFailureAsResult()
        {
            var result = Result.Failure("error");

            result.Should().BeOfType<Failure<string>>();
        }

        [Fact]
        public static void ImplicitSuccessOperator_ReturnsSuccessAsResult()
        {
            var result = (Result<string>) Result.Success;

            result.Should().BeOfType<Success<string>>();
        }

        [Fact]
        public static void ImplicitTFailureOperator_ReturnsFailureAsResult()
        {
            const string failure = "error";

            var resultWithoutTSuccess = (Result<string>) failure;
            var resultWithTSuccess = (Result<string, int>) failure;

            resultWithoutTSuccess.Should().BeOfType<Failure<string>>();
            resultWithTSuccess.Should().BeOfType<Failure<string, int>>();
        }

        [Fact]
        public static void ImplicitTSuccessOperator_ReturnsSuccessAsResult()
        {
            const int success = 1;

            var result = (Result<string, int>) success;

            result.Should().BeOfType<Success<string, int>>();
        }

        [Fact]
        public static void ImplicitFailureWithoutTSuccessTOperator_ReturnsFailureWithTSuccessAsResult()
        {
            var failure = Result.Failure("error");

            var result = (Result<string, int>)failure;

            result.Should().BeOfType<Failure<string, int>>();
        }
    }
}