using System;
using RailSharp.Internal.Option;

namespace RailSharp
{
    public static class OptionExtensions
    {
        /// <summary>
        ///     Execute an <paramref name="action" /> on the contained value only if <paramref name="option" /> contains a value.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="option">The option to execute an <paramref name="action" /> on.</param>
        /// <param name="action">A action function to execute.</param>
        /// <returns>The initial result.</returns>
        public static Option<T> Do<T>(this Option<T> option, Action<T> action)
        {
            if (option is Some<T> some) action(some);
            return option;
        }

        /// <summary>
        ///     Returns a new option containing a mapped value if <paramref name="option" /> contains a value or none if not.
        /// </summary>
        /// <typeparam name="T">The source type of the optional value.</typeparam>
        /// <typeparam name="TDestination">The destination type of the optional value.</typeparam>
        /// <param name="option">The initial option.</param>
        /// <param name="mapper">A function to map the optional value to a new value.</param>
        /// <returns>A new option containing the mapped value or none.</returns>
        public static Option<TDestination> Map<T, TDestination>(this Option<T> option, Func<T, TDestination> mapper) =>
            option is Some<T> some ? (Option<TDestination>) mapper(some) : Option.None;

        /// <summary>
        ///     Returns the contained value if <paramref name="option" /> contains a value or a default value if not.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="option">The initial option.</param>
        /// <param name="defaultValue">The default value to return when <paramref name="option" /> is empty.</param>
        /// <returns>The optional value or a default value.</returns>
        public static T Reduce<T>(this Option<T> option, T defaultValue) => Reduce(option, () => defaultValue);

        /// <summary>
        ///     Returns the contained value if <paramref name="option" /> contains a value or a default value if not.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="option">The initial option.</param>
        /// <param name="defaultValueFactory">A function to create the default value.</param>
        /// <returns>The optional value or a default value.</returns>
        public static T Reduce<T>(this Option<T> option, Func<T> defaultValueFactory) =>
            option is Some<T> some ? (T) some : defaultValueFactory();

        /// <summary>
        ///     Returns none if <paramref name="option" /> doesn't satisfy the given condition.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="option">The initial option.</param>
        /// <param name="predicate">A function to test the optional value.</param>
        /// <returns>The initial option or none.</returns>
        public static Option<T> When<T>(this Option<T> option, Func<T, bool> predicate) =>
            option is Some<T> some && predicate(some) ? (Option<T>) some : Option.None;
    }
}