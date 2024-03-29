﻿using System;
using System.Text.RegularExpressions;

namespace Byteology.GuardClauses;

/// <summary>
/// Contains extension methods for <see cref="IGuardClause{T}"/> whose generic type argument is <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument is null or empty.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <exception cref="ArgumentException">The argument is null or empty.</exception>
	public static IGuardClause<string> NotNullOrEmpty(this IGuardClause<string?> clause)
	{
		if (string.IsNullOrEmpty(clause.Argument))
			throw new ArgumentException($"{clause.ArgumentName} should not be null or empty.");

		return clause!;
	}

	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument is null or empty or consists of only white-space characters.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <exception cref="ArgumentException">The argument is null or empty
	/// or consists of only white-space characters.</exception>
	public static IGuardClause<string> NotNullOrWhiteSpace(this IGuardClause<string?> clause)
	{
		if (string.IsNullOrWhiteSpace(clause.Argument))
			throw new
				ArgumentException($"{clause.ArgumentName} should not be null or empty and it should not consist of only white-space characters.");

		return clause!;
	}

	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument is not in the format specified by a regular expression.
	/// Throws an <see cref="ArgumentNullException"/> if the argument or the pattern are <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <param name="regexPattern">The regular expression pattern to match.</param>
	/// <exception cref="ArgumentException">The argument is not in the format specified by a regular expression.</exception>
	/// <exception cref="ArgumentNullException">The argument or provided pattern are <see langword="null"/>.</exception>
	public static IGuardClause<string> InFormat(
		this IGuardClause<string> clause,
		string regexPattern)
	{
		return InFormat(clause, regexPattern, RegexOptions.None);
	}

	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument is not in the format specified by a regular expression.
	/// Throws an <see cref="ArgumentNullException"/> if the argument or the pattern are <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <param name="regexPattern">The regular expression pattern to match.</param>
	/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
	/// <exception cref="ArgumentException">The argument is not in the format specified by a regular expression.</exception>
	/// <exception cref="ArgumentNullException">The argument or provided pattern are <see langword="null"/>.</exception>
	public static IGuardClause<string> InFormat(
		this IGuardClause<string> clause,
		string regexPattern,
		RegexOptions options)
	{
		return InFormat(clause, regexPattern, RegexOptions.None, Regex.InfiniteMatchTimeout);
	}

	/// <summary>
	/// Throws an <see cref="ArgumentException"/> if the argument is not in the format specified by a regular expression.
	/// Throws an <see cref="ArgumentNullException"/> if the argument or the pattern are <see langword="null"/>.
	/// </summary>
	/// <param name="clause">The guard clause containing the argument to guard.</param>
	/// <param name="regexPattern">The regular expression pattern to match.</param>
	/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
	/// <param name="matchTimeout">A time-out interval, or <see cref="System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
	/// <exception cref="ArgumentException">The argument is not in the format specified by a regular expression.</exception>
	/// <exception cref="ArgumentNullException">The argument or provided pattern are <see langword="null"/>.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when no match is found in the specified time interval</exception>
	public static IGuardClause<string> InFormat(
		this IGuardClause<string> clause,
		string regexPattern,
		RegexOptions options,
		TimeSpan matchTimeout)
	{
		Guard.Argument(clause.Argument, clause.ArgumentName).NotNull();
		Guard.Argument(regexPattern, nameof(regexPattern)).NotNull();

		Match match = Regex.Match(clause.Argument!, regexPattern, options, matchTimeout);
		if (!match.Success || clause.Argument != match.Value)
			throw new ArgumentException($"{clause.ArgumentName} should be in {regexPattern} format.");

		return clause;
	}
}
