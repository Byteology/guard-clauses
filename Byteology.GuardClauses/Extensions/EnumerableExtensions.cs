using System;
using System.Collections.Generic;
using System.Linq;

namespace Byteology.GuardClauses
{
    /// <summary>
    /// Contains extension methods for <see cref="IGuardClause{T}"/> whose generic type argument is <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Throws an <see cref="Exception"/> if the number of elements in the argument does not pass the specified guard clause.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="guardClause">The guard clause that the number of elements in the argument should satisfy.</param>
        public static IGuardClause<IEnumerable<T>> ElementsCount<T>(this IGuardClause<IEnumerable<T>> clause, Action<IGuardClause<int>> guardClause)
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            int elementsCount = clause.Argument.Count();
            string name = $"The number of elements in {clause.ArgumentName}";
            guardClause.Invoke(new GuardClause<int>(elementsCount, name));

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="AggregateException"/> if at least one element of the argument does not pass the specified guard clause.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="guardClause">The guard clause that each elements in the argument should satisfy.</param>
        /// <exception cref="AggregateException"></exception>
        public static IGuardClause<IEnumerable<T>> AllElements<T>(
            this IGuardClause<IEnumerable<T>> clause,
            Action<IGuardClause<T>> guardClause)
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            List<Exception> exceptions = new();

            int index = 0;
            foreach (T element in clause.Argument)
            {
                string name = $"{clause.ArgumentName}[{index}]";
                try
                {
                    guardClause.Invoke(new GuardClause<T>(element, name));
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
                index++;
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            return clause;
        }
    }
}
