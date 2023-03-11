using System;
using System.Collections.Generic;

namespace Console.Contacts;
internal static class LinqExtensions
{
    public static IEnumerable<TResult> MySelect<TInput, TResult>(this IEnumerable<TInput> sequence, Func<TInput, TResult> selector)
    {
        foreach (TInput item in sequence)
        {
            TResult result = selector(item);
            yield return result;
        }
    }

    public static IEnumerable<TInput> MyWhere<TInput>(this IEnumerable<TInput> sequence, Func<TInput, bool> predicate)
    {
        foreach (TInput item in sequence)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
}
