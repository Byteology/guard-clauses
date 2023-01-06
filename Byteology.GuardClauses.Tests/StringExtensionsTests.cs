using System;
using Xunit;

namespace Byteology.GuardClauses.Tests;

public class StringExtensionsTests
{
	[Theory]
	[InlineData("asd", false)]
	[InlineData("  ", false)]
	[InlineData("", true)]
	[InlineData(null, true)]
	public void NotNullOrEmpty(string data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).NotNullOrEmpty();

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Theory]
	[InlineData("asd", false)]
	[InlineData("  ", true)]
	[InlineData("", true)]
	[InlineData(null, true)]
	public void NotNullOrWhiteSpace(string data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).NotNullOrWhiteSpace();

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Theory]
	[InlineData("assassin", "(ass)+in", false)]
	[InlineData("assassin", "(ass)+", true)]
	[InlineData("", "a+", true)]
	[InlineData("", null, true)]
	[InlineData(null, "a+", true)]
	public void InFormat(string? data, string? pattern, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).InFormat(pattern!);

		if (shouldThrow)
		{
			if (data == null || pattern == null)
				Assert.Throws<ArgumentNullException>(action);
			else
				Assert.Throws<ArgumentException>(action);
		}
		else
			action();
	}
}
