using System;
using FluentAssertions;
using RailSharp.Internal.Option;
using Xunit;

namespace RailSharp.Tests
{
    public class OptionTests
    {
        [Fact]
        public static void From_WithNonNull_ReturnsSomeAsOption()
        {
            var option = Option.From("foo");

            option.Should().BeOfType<Some<string>>();
        }

        [Fact]
        public static void From_WithNull_ReturnsNoneAsOption()
        {
            var option = Option.From<string>(null);

            option.Should().BeOfType<None<string>>();
        }

        [Fact]
        public static void ImplicitNoneOperator_ReturnsNoneAsOption()
        {
            var option = (Option<string>)Option.None;

            option.Should().BeAssignableTo<None<string>>();
        }

        [Fact]
        public static void ImplicitTOperator_WithNonNull_ReturnsSomeAsOption()
        {
             var option = (Option<string>)"foo";

            option.Should().BeOfType<Some<string>>();
        }

        [Fact]
        public static void ImplicitTOperator_WithNull_ReturnsNoneAsOption()
        {
            string @null = null; 
            // ReSharper disable once ExpressionIsAlwaysNull
            var option = (Option<string>)@null;

            option.Should().BeOfType<None<string>>();
        }

        [Fact]
        public static void Some_WithNonNull_ReturnsSomeAsOption()
        {
            var option = Option.Some("foo");

            option.Should().BeOfType<Some<string>>();
        }

        [Fact]
        public static void Some_WithNull_Throws()
        {
            Action act = () => Option.Some<string>(null);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}