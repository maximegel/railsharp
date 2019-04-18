using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    public abstract class Result<TFailure, TSuccess>
    {
        public static implicit operator Result<TFailure, TSuccess>(TFailure failure) =>
            new Failure<TFailure, TSuccess>(failure);

        public static implicit operator Result<TFailure, TSuccess>(Failure<TFailure> failure) =>
            new Failure<TFailure, TSuccess>(failure);

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success) =>
            new Success<TFailure, TSuccess>(success);

        public static implicit operator Result<TFailure, TSuccess>(Success<TSuccess> success) =>
            new Success<TFailure, TSuccess>(success);

        public Result<TFailure, TSuccess> Catch<TCatch>(Func<TCatch, TSuccess> mapper)
            where TCatch : TFailure =>
            Catch(_ => true, mapper);

        /// <summary>
        ///     Catches a failure of type <typeparamref name="TCatch" /> and maps it to a success if it matches the given
        ///     <paramref name="predicate" />.
        /// </summary>
        /// <remarks>
        ///     This method is part of this class instead of being an extension method for the sake of syntactic sugar only. We do
        ///     not want to provide <typeparamref name="TFailure" /> and <typeparamref name="TSuccess" /> when calling this method.
        /// </remarks>
        /// <typeparam name="TCatch">The type of failure to catch.</typeparam>
        /// <param name="predicate"></param>
        /// <param name="mapper">The mapping function to execute.</param>
        /// <returns>The mapped failure if the failure has been caugth or the original success if not.</returns>
        public Result<TFailure, TSuccess> Catch<TCatch>(Func<TCatch, bool> predicate, Func<TCatch, TSuccess> mapper)
            where TCatch : TFailure =>
            this.Catch(
                failure => failure is TCatch caugthFailure && predicate(caugthFailure),
                failure => mapper((TCatch) failure));
    }
}