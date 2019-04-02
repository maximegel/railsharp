using System;
using RailSharp.Internal.Either;

namespace RailSharp
{
    public static class EitherExtensions
    {
        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, TNewRight> mapper) =>
            either is Right<TLeft, TRight> right
                ? (Either<TLeft, TNewRight>) mapper(right)
                : (TLeft) (Left<TLeft, TRight>) either;

        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, Either<TLeft, TNewRight>> mapper) =>
            either is Right<TLeft, TRight> right
                ? mapper(right)
                : (TLeft) (Left<TLeft, TRight>) either;

        public static TRight Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> mapper) =>
            either is Left<TLeft, TRight> left
                ? mapper(left)
                : (Right<TLeft, TRight>) either;

        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> mapper, Func<TLeft, bool> predicate) =>
            either is Left<TLeft, TRight> bound && predicate(bound)
                ? (Either<TLeft, TRight>) mapper(bound)
                : either;
    }
}