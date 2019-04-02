using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class ResultExtensions
    {
        public static Result<TFailure, TSuccess> Map<TFailure, TSuccess>(
            this Result<TFailure> result, Func<TSuccess> mapper) =>
            result is Success<TFailure>
                ? (Result<TFailure, TSuccess>) mapper()
                : (TFailure) (Failure<TFailure>) result;

        public static Result<TFailure, TNewSuccess> Map<TFailure, TSuccess, TNewSuccess>(
            this Result<TFailure, TSuccess> result, Func<TSuccess, TNewSuccess> mapper) =>
            result is Success<TFailure, TSuccess> success
                ? (Result<TFailure, TNewSuccess>) mapper(success)
                : (TFailure) (Failure<TFailure, TSuccess>) result;

        public static Result<TNewSuccess, TFailure> Map<TSuccess, TNewSuccess, TFailure>(
            this Result<TFailure, TSuccess> result, Func<TSuccess, Result<TNewSuccess, TFailure>> mapper) =>
            result is Success<TFailure, TSuccess> success
                ? mapper(success)
                : (TFailure) (Failure<TFailure, TSuccess>) result;

        public static TSuccess Reduce<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result, Func<TFailure, TSuccess> mapper) =>
            result is Failure<TFailure, TSuccess> error
                ? mapper(error)
                : (Success<TFailure, TSuccess>) result;

        public static Result<TFailure, TSuccess> Reduce<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result, Func<TFailure, TSuccess> mapper, Func<TFailure, bool> predicate) =>
            result is Failure<TFailure, TSuccess> failure && predicate(failure)
                ? (Result<TFailure, TSuccess>) mapper(failure)
                : result;

        public static TSuccess ReduceOrThrow<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result)
            where TFailure : Exception =>
            Reduce(result, ex => throw ex);
    }
}