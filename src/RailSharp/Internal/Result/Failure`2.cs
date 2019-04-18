namespace RailSharp.Internal.Result
{
    public class Failure<TFailure, TSuccess> : Result<TFailure, TSuccess>
    {
        private readonly TFailure _content;

        public Failure(TFailure content) =>
            _content = content;

        public static implicit operator TFailure(Failure<TFailure, TSuccess> failure) =>
            failure._content;
    }
}