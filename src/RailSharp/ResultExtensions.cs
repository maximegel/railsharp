using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class ResultExtensions
    {
        public static TSuccess Catch<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result, Func<TFailure, TSuccess> mapper) =>
            result is Failure<TFailure, TSuccess> failure
                ? mapper(failure)
                : (Success<TFailure, TSuccess>) result;

        public static Result<TFailure, TSuccess> Catch<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result, Func<TFailure, bool> predicate, Func<TFailure, TSuccess> mapper) =>
            result is Failure<TFailure, TSuccess> failure && predicate(failure)
                ? (Result<TFailure, TSuccess>) mapper(failure)
                : result;

        public static Result<TFailure, TSuccess> Map<TFailure, TSuccess>(
            this Result<TFailure> result, Func<TSuccess> factory) =>
            result is Failure<TFailure> failure
                ? failure
                : (Result<TFailure, TSuccess>) factory();

        public static Result<TFailure, TNewSuccess> Map<TFailure, TSuccess, TNewSuccess>(
            this Result<TFailure, TSuccess> result, Func<TSuccess, TNewSuccess> mapper) =>
            result is Success<TFailure, TSuccess> success
                ? (Result<TFailure, TNewSuccess>) mapper(success)
                : (TFailure) (Failure<TFailure, TSuccess>) result;

        public static TSuccess ThrowIfFailure<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result, Func<TFailure, Exception> mapper) =>
            Catch(result, failure => throw mapper(failure));

        public static TSuccess ThrowIfFailure<TFailure, TSuccess>(this Result<TFailure, TSuccess> result)
            where TFailure : Exception =>
            ThrowIfFailure(result, failure => failure);
    }
}