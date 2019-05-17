using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    /// <summary>
    ///     Represents a result that is either a failure or a non empty success.
    /// </summary>
    /// <typeparam name="TFailure">The type of the failure.</typeparam>
    /// <typeparam name="TSuccess">The type of the success.</typeparam>
    public abstract class Result<TFailure, TSuccess>
    {
        /// <summary>
        ///     Implicitly casts a failure of type <typeparamref cref="TFailure" /> to a result.
        /// </summary>
        /// <param name="failure"></param>
        public static implicit operator Result<TFailure, TSuccess>(TFailure failure) =>
            new Failure<TFailure, TSuccess>(failure);

        /// <summary>
        ///     Implicitly casts a failure to a result.
        /// </summary>
        /// <param name="failure"></param>
        public static implicit operator Result<TFailure, TSuccess>(Failure<TFailure> failure) =>
            new Failure<TFailure, TSuccess>(failure);

        /// <summary>
        ///     Implicitly casts a success of type <typeparamref cref="TSuccess" /> to a result.
        /// </summary>
        /// <param name="success"></param>
        public static implicit operator Result<TFailure, TSuccess>(TSuccess success) =>
            new Success<TFailure, TSuccess>(success);

        /// <summary>
        ///     Implicitly casts a success to a result.
        /// </summary>
        /// <param name="success"></param>
        public static implicit operator Result<TFailure, TSuccess>(Success<TSuccess> success) =>
            new Success<TFailure, TSuccess>(success);

        /// <summary>
        ///     Returns a mapped success if the result is a failure of type <typeparamref name="TCatch" />
        ///     or the initial result if not.
        /// </summary>
        /// <typeparam name="TCatch">The type of failure to catch.</typeparam>
        /// <param name="mapper">A mapping function to map the caught failure to a success.</param>
        /// <returns>The mapped success if the failure has been caugth or the initial result if not.</returns>
        /// <remarks>
        ///     This method is part of this class instead of being an extension method for the sake of syntactic sugar only. We do
        ///     not want to provide <typeparamref name="TFailure" /> and <typeparamref name="TSuccess" /> when calling this method.
        /// </remarks>
        public Result<TFailure, TSuccess> Catch<TCatch>(Func<TCatch, TSuccess> mapper)
            where TCatch : TFailure =>
            Catch(_ => true, mapper);

        /// <summary>
        ///     Returns a mapped success if the result is a failure of type <typeparamref name="TCatch" /> and satisfied the
        ///     <paramref name="predicate" /> or the initial result if not.
        /// </summary>
        /// <typeparam name="TCatch">The type of failure to catch.</typeparam>
        /// <param name="predicate">A filter function that dertermines if the failure shoud be caught.</param>
        /// <param name="mapper">A mapping function to map the caught failure to a success.</param>
        /// <returns>The mapped failure if the failure has been caugth or the initial success if not.</returns>
        /// <remarks>
        ///     This method is part of this class instead of being an extension method for the sake of syntactic sugar only. We do
        ///     not want to provide <typeparamref name="TFailure" /> and <typeparamref name="TSuccess" /> when calling this method.
        /// </remarks>
        public Result<TFailure, TSuccess> Catch<TCatch>(Func<TCatch, bool> predicate, Func<TCatch, TSuccess> mapper)
            where TCatch : TFailure =>
            this.Catch(
                failure => failure is TCatch caugthFailure && predicate(caugthFailure),
                failure => mapper((TCatch) failure));
    }
}