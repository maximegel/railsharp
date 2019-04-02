using RailSharp.Internal.Option;

namespace RailSharp
{
    public static class Option
    {
        public static None None { get; } =
            new None();

        public static Option<T> From<T>(T value) =>
            value == null ? None : Some(value);

        public static Option<T> Some<T>(T value) =>
            new Some<T>(value);
    }
}