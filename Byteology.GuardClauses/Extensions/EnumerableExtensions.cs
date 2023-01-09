using System;
using System.Collections;
using System.Collections.Generic;

namespace Byteology.GuardClauses;

/// <summary>
/// Contains extension methods for <see cref="IGuardClause{T}"/> whose generic type argument is <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument contains any elements.
	/// Does not throw an exception if the argument is <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <exception cref="ArgumentException">The argument contains any elements.</exception>
	public static IGuardClause<T> Empty<T>(this IGuardClause<T> clause)
		where T : class, IEnumerable
	{
		Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

		if (clause.Argument!.any())
			throw new ArgumentException($"{clause.ArgumentName} should be empty.");

		return clause;
	}

	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument does not contain any elements.
	/// Does not throw an exception if the argument is <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <exception cref="ArgumentException">The argument does not contain any elements.</exception>
	public static IGuardClause<T> NotEmpty<T>(this IGuardClause<T> clause)
		where T : class, IEnumerable
	{
		Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

		if (!clause.Argument!.any())
			throw new ArgumentException($"{clause.ArgumentName} should not be empty.");

		return clause;
	}

	/// <summary>
	/// Passes the argument's elements count to the specified guard clause action.
	/// Does not throw an exception if the argument is <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <param name="guardClause">The guard clause that the number of elements in the argument should satisfy.</param>
	public static IGuardClause<T> ElementsCount<T>(
		this IGuardClause<T> clause,
		Action<IGuardClause<int>> guardClause)
		where T : class, IEnumerable
	{
		Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

		int elementsCount = clause.Argument!.count();

		string name = $"The number of elements in {clause.ArgumentName}";
		guardClause.Invoke(new GuardClause<int>(elementsCount, name));

		return clause;
	}

	/// <summary>
	/// Throws an <see cref="AggregateException"/> if at least one element of the
	/// argument does not pass the specified guard clause.
	/// Does not throw an exception if the argument is <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <param name="guardClause">The guard clause that each elements in the argument should satisfy.</param>
	/// <exception cref="AggregateException">At least one element of the
	/// argument does not pass the specified guard clause.</exception>
	public static IGuardClause<IEnumerable<T>> AllElements<T>(
		this IGuardClause<IEnumerable<T>> clause,
		Action<IGuardClause<T>> guardClause)
	{
		Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();

		List<Exception> exceptions = new();

		int index = 0;
		foreach (T element in clause.Argument!)
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

	private static int count(this IEnumerable source)
	{
		if (source is ICollection collection)
			return collection.Count;

		int result = 0;
		IEnumerator enumerator = source.GetEnumerator();

		while (enumerator.MoveNext())
			result++;

		return result;
	}

	private static bool any(this IEnumerable source)
	{
		if (source is ICollection collection)
			return collection.Count != 0;

		IEnumerator enumerator = source.GetEnumerator();
		return enumerator.MoveNext();
	}
}
