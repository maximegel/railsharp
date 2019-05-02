using System;
using System.Collections.Generic;
using System.Linq;
using RailSharp.Internal.Option;

namespace RailSharp
{
    /// <summary>
    ///     Provides extension methods to some commonly used types that take or return an <see cref="Option{T}" />.
    /// </summary>
    public static class OptionAdapters
    {
        /// <summary>
        ///     Filters the sequence of options to keep only the values contained by the options. All empty options are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the optional values.</typeparam>
        /// <param name="sequence">The sequence of options.</param>
        /// <returns>The filtered sequence of values.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<Option<T>> sequence) =>
            sequence.OfType<Some<T>>().Select(option => option.Reduce(default(T)));

        /// <summary>
        ///     Returns an option containing the first element of the sequence or an empty option if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">An <see cref="IEnumerable{T}" /> to return an element from.</param>
        /// <returns>
        ///     An empty option if <paramref name="sequence" /> is empty or an option containing the first element in
        ///     <paramref name="sequence" />.
        /// </returns>
        public static Option<T> TryFirst<T>(this IEnumerable<T> sequence) =>
            TryFirst(sequence, _ => true);

        /// <summary>
        ///     Returns an option containing the first element of the sequence that satisfies a condition or an empty option if no
        ///     such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">An <see cref="IEnumerable{T}" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     An empty option if <paramref name="sequence" /> is empty or if no element passes the test
        ///     specified by <paramref name="predicate" />; otherwise, an option containing the first element in
        ///     <paramref name="sequence" /> that passes the test specified by <paramref name="predicate" />.
        /// </returns>
        /// <remarks>
        ///     This implementation was inspired by Microsoft's implementation of FirstOrDefault. We can't use FirstOrDefault
        ///     direcly in this method because, when using a sequence of structs, we can't determine if the default value is the
        ///     matching element or the actual default value.
        /// </remarks>
        public static Option<T> TryFirst<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            foreach (var element in sequence.Where(predicate))
                return element;

            return Option.None;
        }

        /// <summary>
        ///     Gets the value associated with the specified key or an empty option if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to search in.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        ///     An option with some value if the dictionary contains an element with the specified key; otherwise, an empty
        ///     option.
        /// </returns>
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out var value)
                ? (Option<TValue>) value
                : Option.None;

        /// <summary>
        ///     Returns an option containing the only element of the sequence or an empty option if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">An <see cref="IEnumerable{T}" /> to return an element from.</param>
        /// <returns>
        ///     An empty option if <paramref name="sequence" /> is empty or an option containing the only element in
        ///     <paramref name="sequence" />.
        /// </returns>
        public static Option<T> TrySingle<T>(this IEnumerable<T> sequence) =>
            TrySingle(sequence, _ => true);

        /// <summary>
        ///     Returns an option containing the only element of the sequence that satisfies a condition or an empty option if no
        ///     such element is found.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">An <see cref="IEnumerable{T}" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     An empty option if <paramref name="sequence" /> is empty or if no element or many elements passes the test
        ///     specified by <paramref name="predicate" />; otherwise, an option containing the only element in
        ///     <paramref name="sequence" /> that passes the test specified by <paramref name="predicate" />.
        /// </returns>
        /// <remarks>
        ///     This implementation was inspired by Microsoft's implementation of SingleOrDefault. We can't use SingleOrDefault
        ///     direcly in this method because, when using a sequence of structs, we can't determine if the default value is the
        ///     matching element or the actual default value.
        /// </remarks>
        public static Option<T> TrySingle<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            using (var enumerator = sequence.Where(predicate).GetEnumerator())
            {
                // If the filtered sequence is empty, \
                if (!enumerator.MoveNext())
                    // there is no matching element.
                    return Option.None;

                var result = enumerator.Current;

                // If the filtered sequence have one matching element, \
                if (!enumerator.MoveNext())
                    // we return it.
                    return Option.Some(result);
            }

            return Option.None;
        }

        /// <summary>
        ///     Returns an option containing <paramref name="value" /> if it satisfies a condition or an empty option if not.
        /// </summary>
        /// <typeparam name="T">The type of the tested value.</typeparam>
        /// <param name="value">The value to test against a condition.</param>
        /// <param name="predicate">A function to test the value.</param>
        /// <returns>
        ///     An empty option if <paramref name="value" /> doesn't pass the test specified by <paramref name="predicate" />;
        ///     otherwise, an option containing <paramref name="value" />.
        /// </returns>
        public static Option<T> When<T>(this T value, Func<T, bool> predicate) =>
            predicate(value) ? (Option<T>) value : Option.None;
    }
}