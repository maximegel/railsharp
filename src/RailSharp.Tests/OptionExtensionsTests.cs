using FluentAssertions;
using RailSharp.Internal.Option;
using Xunit;

namespace RailSharp.Tests
{
    public class OptionExtensionsTests
    {
        [Fact]
        public void Do_WhenNone_DoesntInvokeAction()
        {
            var count = 0;
            var option = Option<int>.None;

            option
                .Do(x => count++)
                .Reduce(0);

            count.Should().Be(0);
        }

        [Fact]
        public void Do_WhenSome_InvokesAction()
        {
            var count = 0;
            var option = Option.Some(5);

            option
                .Do(x => count++)
                .Reduce(0);

            count.Should().Be(1);
        }

        [Fact]
        public void Map_WhenNone_DoesntMapContent()
        {
            var option = Option<int>.None;

            var result = option
                .Map(x => $"content={5}")
                .Reduce("content=none");

            result.Should().Be("content=none");
        }

        [Fact]
        public void Map_WhenSome_MapsContent()
        {
            var option = Option.Some(5);

            var result = option
                .Map(x => $"content={5}")
                .Reduce("content=none");

            result.Should().Be("content=5");
        }

        [Fact]
        public void Reduce_WithDefaultFuncWhenNone_ReturnsDefaultFuncResult()
        {
            var option = Option<int>.None;

            var result = option.Reduce(() => 5);

            result.Should().Be(5);
        }

        [Fact]
        public void Reduce_WithDefaultFuncWhenSome_ReturnsContent()
        {
            var option = Option.Some(5);

            var result = option.Reduce(() => 1);

            result.Should().Be(5);
        }

        [Fact]
        public void Reduce_WithDefaultValueWhenNone_ReturnsDefaultValue()
        {
            var option = Option<int>.None;

            var result = option.Reduce(5);

            result.Should().Be(5);
        }

        [Fact]
        public void Reduce_WithDefaultValueWhenSome_ReturnsContent()
        {
            var option = Option.Some(5);

            var result = option.Reduce(1);

            result.Should().Be(5);
        }

        [Fact]
        public void When_WhenNone_ReturnsNone()
        {
            var option = Option<int>.None;

            var result = option.When(x => x > 3);

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public void When_WithMatchingPredicateWhenSome_ReturnsSome()
        {
            var option = Option.Some(5);

            var result = option.When(x => x > 3);

            result.Should().BeOfType<Some<int>>();
        }

        [Fact]
        public void When_WithNonMatchingPredicateWhenSome_ReturnsNone()
        {
            var option = Option.Some(5);

            var result = option.When(x => x > 8);

            result.Should().BeOfType<None<int>>();
        }
    }
}