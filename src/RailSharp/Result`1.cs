using RailSharp.Internal.Result;

namespace RailSharp
{
    /// <summary>
    ///     Represents a result that is either a failure or an empty success.
    /// </summary>
    /// <typeparam name="TFailure">The type of the failure.</typeparam>
    public abstract class Result<TFailure>
    {
        /// <summary>
        ///     Implicitly casts a value of any type to a generic result failure of the given <paramref name="value" /> type.
        /// </summary>
        /// <param name="value">The failure to cast.</param>
        public static implicit operator Result<TFailure>(TFailure value) =>
            new Failure<TFailure>(value);

        /// <summary>
        ///     Implicitly casts an empty success to a generic result of the desired failure type.
        /// </summary>
        /// <param name="success">The success to cast.</param>
        public static implicit operator Result<TFailure>(VoidSuccess success) =>
            new VoidSuccess<TFailure>();
    }
}