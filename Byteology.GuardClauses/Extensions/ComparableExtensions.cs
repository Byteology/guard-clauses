using System;

namespace Byteology.GuardClauses
{
    /// <summary>
    /// Contains extension methods for <see cref="IGuardClause{T}"/> whose generic type argument is <see cref="IComparable{T}"/>.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is not greater than the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> GreaterThan<T>(this IGuardClause<T> clause, T other)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            if (clause.Argument.CompareTo(other) <= 0)
                throw new ArgumentException($"{clause.ArgumentName} should be greater than {other}.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is not greater than or equal to the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> GreaterThanOrEqualTo<T>(this IGuardClause<T> clause, T other)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            if (clause.Argument.CompareTo(other) < 0)
                throw new ArgumentException($"{clause.ArgumentName} should be greater than or equal to {other}.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is not less than the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> LessThan<T>(this IGuardClause<T> clause, T other)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            if (clause.Argument.CompareTo(other) >= 0)
                throw new ArgumentException($"{clause.ArgumentName} should be less than {other}.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is not less than or equal to the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> LessThanOrEqualTo<T>(this IGuardClause<T> clause, T other)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            if (clause.Argument.CompareTo(other) > 0)
                throw new ArgumentException($"{clause.ArgumentName} should be less than or equal to {other}.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is not within the specified 
        /// closed interval.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuardClause<T> InRange<T>(this IGuardClause<T> clause, T min, T max)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            int minCompare = clause.Argument.CompareTo(min);
            int maxCompare = clause.Argument.CompareTo(max);

            if (minCompare < 0 || maxCompare > 0)
                throw new ArgumentOutOfRangeException($"{clause.ArgumentName} should be in the range [{min},{max}].");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is not within the specified 
        /// closed interval.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuardClause<T> NotInRange<T>(this IGuardClause<T> clause, T min, T max)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

            int minCompare = clause.Argument.CompareTo(min);
            int maxCompare = clause.Argument.CompareTo(max);

            if (minCompare >= 0 && maxCompare <= 0)
                throw new ArgumentOutOfRangeException($"{clause.ArgumentName} should not be in the range [{min},{max}].");

            return clause;
        }
    }
}
