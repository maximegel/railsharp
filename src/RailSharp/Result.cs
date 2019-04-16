using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class Result
    {
        public static Success Success =>
            new Success();

        public static Failure<TFailure> Failure<TFailure>(TFailure failure) =>
            new Failure<TFailure>(failure);
    }
}