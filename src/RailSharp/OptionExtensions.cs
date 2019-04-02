using System;
using System.Threading.Tasks;
using RailSharp.Internal.Option;

namespace RailSharp
{
    public static class OptionExtensions
    {
        public static Option<T> Do<T>(this Option<T> option, Action<T> action)
        {
            if (option is Some<T> some)
                action(some);
            return option;
        }

        public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> mapper) =>
            option is Some<T> some ? (Option<TResult>) mapper(some) : Option.None;

        public static T Reduce<T>(this Option<T> option, T defaultValue) =>
            Reduce(option, () => defaultValue);

        public static T Reduce<T>(this Option<T> option, Func<T> defaultValueFactory) =>
            option is Some<T> some ? (T) some : defaultValueFactory();

        public static Task<T> ReduceAsync<T>(this Option<Task<T>> option, T defaultValue) =>
            Reduce(option, Task.FromResult(defaultValue));

        public static Task<T> ReduceAsync<T>(this Option<Task<T>> option, Func<T> defaultValueFactory) =>
            Reduce(option, () => Task.FromResult(defaultValueFactory()));

        public static Option<T> When<T>(this Option<T> option, Func<T, bool> predicate) =>
            option is Some<T> some && predicate(some) ? (Option<T>) some : Option.None;
    }
}