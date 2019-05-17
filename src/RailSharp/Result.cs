using RailSharp.Internal.Result;

namespace RailSharp
{
    /// <summary>
    ///     Provides factory methods to create results.
    /// </summary>
    public static class Result
    {
        /// <summary>
        ///     Creates an result which is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the failure.</typeparam>
        /// <param name="value">The failure instance.</param>
        /// <returns>The created result failure.</returns>
        public static Failure<T> Failure<T>(T value) => new Failure<T>(value);

        /// <summary>
        ///     Creates an result which is a success.
        /// </summary>
        /// <typeparam name="T">The type of the success.</typeparam>
        /// <param name="value">The success instance.</param>
        /// <returns>The created result success.</returns>
        public static Success<T> Success<T>(T value) => new Success<T>(value);

        /// <summary>
        ///     Creates an result which is a success containing no success value.
        /// </summary>
        /// <returns>The created result success.</returns>
        public static VoidSuccess Success() => new VoidSuccess();
    }
}