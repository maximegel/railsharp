namespace RailSharp.Internal.Result
{
    public class Success<TFailure, TSuccess> : Result<TFailure, TSuccess>
    {
        private readonly TSuccess _content;

        public Success(TSuccess content) => _content = content;

        public static implicit operator TSuccess(Success<TFailure, TSuccess> success) => success._content;
    }
}