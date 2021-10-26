using System;

namespace Byteology.GuardClauses
{
    /// <summary>
    /// Contains extension methods for <see cref="IGuardClause{T}"/>.
    /// </summary>
    public static class ObjectExtensions
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
        /// <param name="predicateDescription">The description of the predicate. It will show in the exception message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static IGuardClause<T> Satisfies<T>(
            this IGuardClause<T> clause, 
            Func<T, bool> predicate, 
            string predicateDescription = null)
        {
            string exceptionMessage;
            if (predicateDescription == null)
                exceptionMessage = $"{clause.ArgumentName} should satisfy the specified predicate.";
            else
                exceptionMessage = $"{clause.ArgumentName} should satisfy the following condition: \"{predicateDescription}\".";

            if (!predicate.Invoke(clause.Argument))
                throw new ArgumentException(exceptionMessage);

            return clause;
        }
    }
}
