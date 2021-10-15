using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Byteology.GuardClauses
{
    /// <summary>
    /// Contains extension methods for <see cref="IGuardClause{T}"/>.
    /// </summary>
    public static class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the argument is <see langword="null"/>.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static IGuardClause<T> NotNull<T>(this IGuardClause<T> clause)
        {
            if (clause.Argument == null)
                throw new ArgumentNullException($"{clause.ArgumentName} should not be null.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is equal to its default value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> NotDefault<T>(this IGuardClause<T> clause)
        {
            bool shouldThrow = false;
            if (clause.Argument == null)
                shouldThrow = true;
            else
            {
                Type argumentType = clause.Argument.GetType();
                if (argumentType.IsValueType)
                {
                    object defaultValue = Activator.CreateInstance(argumentType);
                    shouldThrow = clause.Argument.Equals(defaultValue);
                }
            }

            if (shouldThrow)
                throw new ArgumentException($"{clause.ArgumentName} should not be equal to its default value.");

            return clause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument does not equal to the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> EqualsTo<T>(this IGuardClause<T> clause, T other)
        {
            ArgumentException ex = new($"{clause.ArgumentName} should be equal to {other}.");

            if (clause.Argument == null)
            {
                if (other != null)
                    throw ex;
            }
            else
            {
                if (!clause.Argument.Equals(other))
                    throw ex;
            }

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is equal to the specified value.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="other">The object to compare with the argument.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> NotEqualsTo<T>(this IGuardClause<T> clause, T other)
        {
            ArgumentException ex = new($"{clause.ArgumentName} should not be equal to {other}.");
            if (clause.Argument == null)
            {
                if (other == null)
                    throw ex;
            }
            else
            {
                if (clause.Argument.Equals(other))
                    throw ex;

            }

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument does not satisfy the specified predicate.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="predicate">The predicate the argument should satisfy.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> Satisfies<T>(this IGuardClause<T> clause, Func<T, bool> predicate)
        {
            if (!predicate.Invoke(clause.Argument))
                throw new ArgumentException($"{clause.ArgumentName} should satisfy the specified predicate.");

            return clause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is null or empty.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<string> NotNullOrEmpty(this IGuardClause<string> clause)
        {
            if (string.IsNullOrEmpty(clause.Argument))
                throw new ArgumentException($"{clause.ArgumentName} should not be null or empty.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is null or empty or consists of only white-space characters.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<string> NotNullOrWhiteSpace(this IGuardClause<string> clause)
        {
            if (string.IsNullOrWhiteSpace(clause.Argument))
                throw new ArgumentException($"{clause.ArgumentName} should not be null or empty and it should not consist of only white-space characters.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the argument is not in the format specified by a regular expression.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="regexPattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<string> InFormat(this IGuardClause<string> clause, string regexPattern, RegexOptions options = RegexOptions.None)
        {
            Guard.Argument(regexPattern, nameof(regexPattern)).NotNull();

            Match match = clause.Argument != null ? Regex.Match(clause.Argument, regexPattern, options) : null;
            if (match?.Success != true || clause.Argument != match.Value)
                throw new ArgumentException($"{clause.ArgumentName} should be in {regexPattern} format.");

            return clause;
        }

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
            Guard.Argument(other, nameof(other)).NotNull();

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
            Guard.Argument(other, nameof(other)).NotNull();

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
            Guard.Argument(other, nameof(other)).NotNull();

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
            Guard.Argument(other, nameof(other)).NotNull();

            if (clause.Argument.CompareTo(other) > 0)
                throw new ArgumentException($"{clause.ArgumentName} should be less than or equal to {other}.");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is not within the specified range.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuardClause<T> InRange<T>(this IGuardClause<T> clause, T min, T max)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();
            Guard.Argument(min, nameof(min)).NotNull();
            Guard.Argument(max, nameof(max)).NotNull();

            int minCompare = clause.Argument.CompareTo(min);
            int maxCompare = clause.Argument.CompareTo(max);

            if (minCompare < 0 || maxCompare > 0)
                throw new ArgumentOutOfRangeException($"{clause.ArgumentName} should be in the range [{min},{max}].");

            return clause;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is within the specified range.
        /// </summary>
        /// <param name="clause">The guard clause containing the argument to guard.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuardClause<T> NotInRange<T>(this IGuardClause<T> clause, T min, T max)
            where T : IComparable<T>
        {
            Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();
            Guard.Argument(min, nameof(min)).NotNull();
            Guard.Argument(max, nameof(max)).NotNull();

            int minCompare = clause.Argument.CompareTo(min);
            int maxCompare = clause.Argument.CompareTo(max);

            if (minCompare >= 0 && maxCompare <= 0)
                throw new ArgumentOutOfRangeException($"{clause.ArgumentName} should not be in the range [{min},{max}].");

            return clause;
        }

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
                catch(Exception ex)
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
