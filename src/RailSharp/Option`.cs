using RailSharp.Internal.Option;
using static RailSharp.Option;

namespace RailSharp
{
    /// <summary>
    ///     Represents a value that may or may not be present.
    /// </summary>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    public abstract class Option<T>
    {
        /// <summary>
        ///     An option containing no value.
        /// </summary>
        public static Option<T> None => new None<T>();

        /// <summary>
        ///     Implicitly casts a value of any type to an option containing <paramref name="value" />.
        /// </summary>
        /// <param name="value">The value to cast.</param>
        public static implicit operator Option<T>(T value) => From(value);

        /// <summary>
        ///     Implicitly casts an empty option to a generic option of the desired type.
        /// </summary>
        /// <param name="none">The value to cast.</param>
        public static implicit operator Option<T>(None none) => new None<T>();
    }
}