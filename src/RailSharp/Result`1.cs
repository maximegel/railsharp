using RailSharp.Internal.Result;
using static RailSharp.Result;

namespace RailSharp
{
    public abstract class Result<TFailure>
    {
        public static implicit operator Result<TFailure>(TFailure failure) =>
            Failure(failure);

        public static implicit operator Result<TFailure>(Success success) =>
            new Success<TFailure>();
    }
}