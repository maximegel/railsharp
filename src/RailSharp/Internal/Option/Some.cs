namespace RailSharp.Internal.Option
{
    public class Some<T> : Option<T>
    {
        private readonly T _content;

        public Some(T content) =>
            _content = content;

        public static implicit operator T(Some<T> value) =>
            value._content;
    }
}