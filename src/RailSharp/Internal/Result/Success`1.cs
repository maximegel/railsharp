namespace RailSharp.Internal.Result
{
    public class Success<TSuccess>
    {
        private readonly TSuccess _content;

        public Success(TSuccess content) =>
            _content = content;

        public static implicit operator TSuccess(Success<TSuccess> success) =>
            success._content;
    }
}