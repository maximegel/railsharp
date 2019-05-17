using System;
using RailSharp.Internal.Option;

namespace RailSharp
{
    /// <summary>
    ///     Provides factory methods to create options.
    /// </summary>
    public static class Option
    {
        /// <summary>
        ///     An option containing no value.
        /// </summary>
        public static None None { get; } =
            new None();

        /// <summary>
        ///     Creates an option containing a value; if <paramref name="value" /> is null, returns <see cref="None" />.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="value">The value that will be contained by the option.</param>
        /// <returns>The created option.</returns>
        public static Option<T> From<T>(T value) => value == null ? None : Some(value);

        /// <summary>
        ///     Creates an option containing a value; if <paramref name="value" /> is null, throws
        ///     <see cref="ArgumentNullException" />.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="value">The value that will be contained by the option.</param>
        /// <returns>The created option.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value" /> is null.</exception>
        public static Option<T> Some<T>(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new Some<T>(value);
        }
    }
}