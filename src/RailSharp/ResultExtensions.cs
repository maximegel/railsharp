using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class ResultExtensions
    {
        /// <summary>
        ///     Returns a mapped success if <paramref name="result" /> is a failure
        ///     or the initial <paramref name="result" /> if not.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The type of the success.</typeparam>
        /// <param name="result">The initial result to catch a failure from.</param>
        /// <param name="mapper">A mapping function to map the caught failure to a success.</param>
        /// <returns>The mapped success if the failure has been caugth or the initial result if not.</returns>
        public static TSuccess Catch<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result,
            Func<TFailure, TSuccess> mapper) =>
            result is Failure<TFailure, TSuccess> failure
                ? mapper(failure)
                : (Success<TFailure, TSuccess>) result;

        /// <summary>
        ///     Returns a mapped success if <paramref name="result" /> is a failure
        ///     or the initial <paramref name="result" /> if not.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The type of the success.</typeparam>
        /// <param name="result">The initial result to catch a failure from.</param>
        /// <param name="predicate">A filter function that dertermines if the failure shoud be caught.</param>
        /// <param name="mapper">A mapping function to map the caught failure to a success.</param>
        /// <returns>The mapped success if the failure has been caugth or the initial result if not.</returns>
        public static Result<TFailure, TSuccess> Catch<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result,
            Func<TFailure, bool> predicate,
            Func<TFailure, TSuccess> mapper) =>
            result is Failure<TFailure, TSuccess> failure && predicate(failure)
                ? (Result<TFailure, TSuccess>) mapper(failure)
                : result;

        /// <summary>
        ///     Returns a success containing a newly created value if <paramref name="result" /> is an empty success
        ///     or the initial failure if not.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The type of the success.</typeparam>
        /// <param name="result">The initial result.</param>
        /// <param name="factory">A function to create the success value.</param>
        /// <returns>The mapped non empty success or the initial failure.</returns>
        public static Result<TFailure, TSuccess> Map<TFailure, TSuccess>(
            this Result<TFailure> result,
            Func<TSuccess> factory) =>
            result is Failure<TFailure> failure
                ? failure
                : (Result<TFailure, TSuccess>) factory();

        /// <summary>
        ///     Returns a mapped success if <paramref name="result" /> is an success or the initial failure if not.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The source type of the success.</typeparam>
        /// <typeparam name="TNewSuccess">The destination type of the initial success.</typeparam>
        /// <param name="result">The initial result.</param>
        /// <param name="mapper">
        ///     A function to map the current success to a new success of type
        ///     <typeparamref name="TNewSuccess" />.
        /// </param>
        /// <returns>The mapped success or the initial failure.</returns>
        public static Result<TFailure, TNewSuccess> Map<TFailure, TSuccess, TNewSuccess>(
            this Result<TFailure, TSuccess> result,
            Func<TSuccess, TNewSuccess> mapper) =>
            result is Success<TFailure, TSuccess> success
                ? (Result<TFailure, TNewSuccess>) mapper(success)
                : (TFailure) (Failure<TFailure, TSuccess>) result;

        /// <summary>
        ///     Throws an exception if <paramref name="result" /> is a failure or returns the success value if it's a success.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The type of the success.</typeparam>
        /// <param name="result">The initial result.</param>
        /// <param name="mapper">A function to map the failure to an exception.</param>
        /// <returns>The success value if <paramref name="result" /> is a success.</returns>
        public static TSuccess ThrowIfFailure<TFailure, TSuccess>(
            this Result<TFailure, TSuccess> result,
            Func<TFailure, Exception> mapper) =>
            Catch(result, failure => throw mapper(failure));

        /// <summary>
        ///     Throws an exception if <paramref name="result" /> is a failure or returns the success value if it's a success.
        /// </summary>
        /// <typeparam name="TFailure">The type of the failure.</typeparam>
        /// <typeparam name="TSuccess">The type of the success.</typeparam>
        /// <param name="result">The initial result.</param>
        /// <returns>The success value if <paramref name="result" /> is a success.</returns>
        public static TSuccess ThrowIfFailure<TFailure, TSuccess>(this Result<TFailure, TSuccess> result)
            where TFailure : Exception =>
            ThrowIfFailure(result, failure => failure);
    }
}