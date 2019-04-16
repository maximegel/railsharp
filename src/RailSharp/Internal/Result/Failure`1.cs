namespace RailSharp.Internal.Result
{
    public class Failure<TFailure> : Result<TFailure>
    {
        internal readonly TFailure _content;

        public Failure(TFailure content) =>
            _content = content;

        public static implicit operator TFailure(Failure<TFailure> failure) =>
            failure._content;
    }
}