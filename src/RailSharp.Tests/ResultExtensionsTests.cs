﻿using System;
using FluentAssertions;
using Xunit;

namespace RailSharp.Tests
{
    public class ResultExtensionsTests
    {
        [Fact]
        public static void Catch_WhenFailure_MapsFailure()
        {
            Result<int, string> result = Result.Failure(500);

            var value = result.Catch(err => $"error: {err}");

            value.Should().Be("error: 500");
        }

        [Fact]
        public static void Catch_WithMatchingPredicate_ReturnsMappedFailure()
        {
            Result<int, string> result = Result.Failure(404);

            var value = result
                .Catch(err => err == 404, err => "error: not found")
                .Catch(err => $"error: {err}");

            value.Should().Be("error: not found");
        }

        [Fact]
        public static void Catch_WithNonMatchingPredicate_DoesntMapFailure()
        {
            Result<int, string> result = Result.Failure(500);

            var value = result
                .Catch(err => err == 404, err => "error: not found")
                .Catch(err => $"error: {err}");

            value.Should().Be("error: 500");
        }

        [Fact]
        public static void Map_WhenFailureWithoutTSuccess_DoesntMapSuccess()
        {
            Result<string> result = Result.Failure("error");

            var value = result
                .Map(() => 5)
                .Catch(_ => 0);

            value.Should().Be(0);
        }

        [Fact]
        public static void Map_WhenFailureWithTSuccess_DoesntMapSuccess()
        {
            Result<string, int> result = Result.Failure("error");

            var value = result
                .Map(x => x + 2)
                .Catch(_ => 0);

            value.Should().Be(0);
        }

        [Fact]
        public static void Map_WhenSuccessWithoutTSuccess_MapsSuccess()
        {
            Result<string> result = Result.Success;

            var value = result
                .Map(() => 5)
                .Catch(_ => 0);

            value.Should().Be(5);
        }

        [Fact]
        public static void Map_WhenSuccessWithTSuccess_MapsSuccess()
        {
            Result<string, int> result = 3;

            var value = result
                .Map(x => x + 2)
                .Catch(_ => 0);

            value.Should().Be(5);
        }

        [Fact]
        public static void ThrowIfFailure_WhenFailure_Throws()
        {
            Result<Exception, int> result = Result.Failure(new Exception("error: 500"));

            Action act = () => result.ThrowIfFailure();

            act.Should().ThrowExactly<Exception>().WithMessage("error: 500");
        }

        [Fact]
        public static void ThrowIfFailure_WhenFailureWithMapper_Throws()
        {
            Result<int, string> result = Result.Failure(500);

            Action act = () => result.ThrowIfFailure(err => new Exception($"error: {err}"));

            act.Should().ThrowExactly<Exception>().WithMessage("error: 500");
        }

        [Fact]
        public static void ThrowIfFailure_WhenSuccess_ReturnsSuccess()
        {
            Result<Exception, int> result = 5;

            var value = result.ThrowIfFailure();

            value.Should().Be(5);
        }

        [Fact]
        public static void ThrowIfFailure_WhenSuccessWithMapper_ReturnsSuccess()
        {
            Result<Exception, int> result = 5;

            var value = result.ThrowIfFailure(err => new Exception($"error: {err}"));

            value.Should().Be(5);
        }
    }
}