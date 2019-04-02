namespace RailSharp.Internal.Either
{
    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Left(TLeft content) =>
            Content = content;

        private TLeft Content { get; }

        public static implicit operator TLeft(Left<TLeft, TRight> obj) =>
            obj.Content;
    }
}