using RailSharp.Internal.Option;

namespace RailSharp
{
    public abstract class Option<T>
    {
        public static Option<T> None =>
            new None<T>();

        public static implicit operator Option<T>(T value) =>
            new Some<T>(value);

        public static implicit operator Option<T>(None none) =>
            new None<T>();
    }
}