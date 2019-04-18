using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class Result
    {
        public static Failure<T> Failure<T>(T value) =>
            new Failure<T>(value);

        public static Success<T> Success<T>(T value) =>
            new Success<T>(value);

        public static VoidSuccess Success() =>
            new VoidSuccess();
    }
}