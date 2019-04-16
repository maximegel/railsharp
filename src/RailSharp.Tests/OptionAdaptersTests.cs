using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RailSharp.Internal.Option;
using Xunit;

namespace RailSharp.Tests
{
    public class OptionAdaptersTests
    {
        [Fact]
        public static void Flatten_ReturnsSequenceOfSomeContent()
        {
            var sequence = new[] {Option.From(3), Option.None, Option.Some(5), Option.None};

            var result = sequence.Flatten().ToList();

            result.Should().BeEquivalentTo(new[] {3, 5});
        }

        [Fact]
        public static void TryFirst_WithEmptySequence_ReturnsNone()
        {
            var sequence = new int[0];

            var result = sequence.TryFirst();

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TryFirst_WithMatchingPredicate_ReturnsFirstMatchAsSome()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TryFirst(x => x > 3);

            result.Should().BeOfType<Some<int>>();
            result.Reduce(0).Should().Be(5);
        }

        [Fact]
        public static void TryFirst_WithNonEmptySequence_ReturnsFirstMatchAsSome()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TryFirst();

            result.Should().BeOfType<Some<int>>();
            result.Reduce(0).Should().Be(3);
        }

        [Fact]
        public static void TryFirst_WithNonMatchingPredicate_ReturnsNone()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TryFirst(x => x > 5);

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TryGetValue_WithExistingKey_ReturnsValueAsSome()
        {
            var dictionary = new Dictionary<int, string> {{3, "foo"}, {5, "bar"}};

            var result = dictionary.TryGetValue(5);

            result.Should().BeOfType<Some<string>>();
            result.Reduce("none").Should().Be("bar");
        }

        [Fact]
        public static void TryGetValue_WithUnexistingKey_ReturnsNone()
        {
            var dictionary = new Dictionary<int, string> {{3, "foo"}, {5, "bar"}};

            var result = dictionary.TryGetValue(4);

            result.Should().BeOfType<None<string>>();
        }

        [Fact]
        public static void TrySingle_WhenManyElements_ReturnsNone()
        {
            var sequence = new[] {3, 2};

            var result = sequence.TrySingle();

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TrySingle_WhenSingleElement_ReturnsAsSome()
        {
            var sequence = new[] {3};

            var result = sequence.TrySingle();

            result.Should().BeOfType<Some<int>>();
            result.Reduce(0).Should().Be(3);
        }

        [Fact]
        public static void TrySingle_WithEmptySequence_ReturnsNone()
        {
            var sequence = new int[0];

            var result = sequence.TrySingle();

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TrySingle_WithNonMatchingPredicate_ReturnsNone()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TrySingle(x => x > 5);

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TrySingle_WithPredicateMatchingMany_ReturnsNone()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TrySingle(x => x > 3);

            result.Should().BeOfType<None<int>>();
        }

        [Fact]
        public static void TrySingle_WithPredicateMatchingOne_ReturnsMatchAsSome()
        {
            var sequence = new[] {3, 2, 5, 4};

            var result = sequence.TrySingle(x => x == 5);

            result.Should().BeOfType<Some<int>>();
            result.Reduce(0).Should().Be(5);
        }

        [Fact]
        public static void When_WithMatchingPredicate_ReturnsSome()
        {
            const int value = 5;

            var result = value.When(x => x == 5);

            result.Should().BeOfType<Some<int>>();
            result.Reduce(0).Should().Be(5);
        }

        [Fact]
        public static void When_WithNonMatchingPredicate_ReturnsNone()
        {
            const int value = 5;

            var result = value.When(x => x == 3);

            result.Should().BeOfType<None<int>>();
        }
    }
}