using System;
using System.Collections.Generic;
using System.Linq;
using SmartClasses.Utils;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static IEnumerable<Node<T>> Hierarchize<T, TKey, TOrderKey>(
        this IEnumerable<T> elements,
            TKey topMostKey,
            Func<T, TKey> keySelector,
            Func<T, TKey> parentKeySelector,
            Func<T, TOrderKey> orderingKeySelector)
        {
            var families = elements.ToLookup(parentKeySelector);
            var childrenFetcher = default(Func<TKey, IEnumerable<Node<T>>>);
            childrenFetcher = parentId => families[parentId]
                .OrderBy(orderingKeySelector)
                .Select(x => new Node<T>(x, childrenFetcher(keySelector(x))));

            return childrenFetcher(topMostKey);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static IEnumerable<T> Descendants<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> descendBy)
        {
            if (!source.IsNullOrEmpty())
            {
                foreach (T value in source)
                {
                    yield return value;

                    if (!descendBy(value).IsNullOrEmpty())
                    {
                        foreach (T child in descendBy(value).Descendants<T>(descendBy))
                        {
                            yield return child;
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> Descendants<T, TKey>(
            this IEnumerable<T> source,
            TKey startWithKey,
            Func<T, TKey> keySelector,
            Func<T, TKey> parentKeySelector)
        {
            var elements = source as IList<T> ?? source.ToList();

            var item = elements.SingleOrDefault(x => keySelector(x).Equals(startWithKey));
            yield return item;

            var children = elements.Where(x =>
            {
                var key = parentKeySelector(x);
                return key == null ? startWithKey == null : key.Equals(startWithKey);
            }).ToList();

            foreach (var itm in children)
            {
                var subDescedants = children.Descendants(keySelector(itm), keySelector, parentKeySelector);
                foreach (var d in subDescedants)
                    yield return d;
                
            }

        }
    }
}
