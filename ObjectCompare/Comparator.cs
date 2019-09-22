using System;
using System.Collections;

namespace ObjectCompare
{
    /// <summary>
    /// 
    /// </summary>
    public static class Comparator
    {
        /// <summary>
        /// Performs a deep inspection of two objects for equality without
        /// relying on string equality and instead falling back on reflection
        /// for analysis.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Matches(object a, object b)
        {
            // true if they are literally the same object
            // also covers two null objects
            if (ReferenceEquals(a, b)) return true;

            // if either item is null return false immediately
            if (a == null || b == null)
            {
                return false;
            }

            // ensure types are equal
            if (a.GetType() != b.GetType()) return false;

            // there is no where further down in the chain to proceed
            // return current result
            if (IsPrimitiveType(a.GetType()))
            {
                return a.Equals(b);
            }

            // if we have a collection iterate on the collection
            // there is no further action that can be taken
            if (a.GetType().IsArray)
            {
                return ArrayMatches((Array) a, (Array) b);
            }

            // an array is different from an enumerable
            if (a is IEnumerable)
            {
                return ListMatches((IEnumerable) a, (IEnumerable) b);
            }

            // start property inspection. at any point if a property does not
            // match we can immediately return false
            foreach (var property in a.GetType().GetProperties())
            {
                if (!Matches(property.GetValue(a), property.GetValue(b)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionA"></param>
        /// <param name="collectionB"></param>
        /// <returns></returns>
        public static bool ArrayMatches(Array collectionA, Array collectionB)
        {
            // we can take advantage of knowing this is an array to short circuit
            // if we do not have two arrays of equal size
            if (collectionA.Length != collectionB.Length)
            {
                return false;
            }

            return ListMatches(collectionA, collectionB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionA"></param>
        /// <param name="collectionB"></param>
        /// <returns></returns>
        public static bool ListMatches(IEnumerable collectionA,
            IEnumerable collectionB)
        {
            var enumeratorA = collectionA.GetEnumerator();
            var enumeratorB = collectionB.GetEnumerator();

            while (true)
            {
                var aHasValue = enumeratorA.MoveNext();
                var bHasValue = enumeratorB.MoveNext();

                // if no more items exist in either chain we can assume that the
                // third if statement has not given us cause to return false.
                // we can also assume that all items till this point have matched
                if (!aHasValue && !bHasValue)
                {
                    return true;
                }

                // difference in list size means we do not have a match.
                // return immediately
                if (aHasValue != bHasValue)
                {
                    return false;
                }

                // only after a item does not match can we return. we have to
                // inspect the entire list till we can return or match everything
                if (!Matches(enumeratorA.Current, enumeratorB.Current))
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(Type t)
        {
            return t.IsPrimitive || t.IsValueType || (t == typeof(string));
        }
    }
}