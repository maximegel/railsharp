using RailSharp.Internal.Option;
using static RailSharp.Option;

namespace RailSharp
{
    public abstract class Option<T>
    {
        public static Option<T> None =>
            new None<T>();

        public static implicit operator Option<T>(T value) =>
            From(value);

        public static implicit operator Option<T>(None none) =>
            new None<T>();
    }
}