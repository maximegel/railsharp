namespace RailSharp.Internal.Either
{
    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Right(TRight content) =>
            Content = content;

        private TRight Content { get; }

        public static implicit operator TRight(Right<TLeft, TRight> obj) =>
            obj.Content;
    }
}