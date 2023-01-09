using System;
using System.Collections;
using Xunit;

namespace Byteology.GuardClauses.Tests;

public class EnumerableExtensionsTests
{
	[Theory]
	[InlineData(new[] { 1 }, true)]
	[InlineData(new int[] { }, false)]
	public void Empty(int[] data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).Empty();

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Fact]
	public void EmptyOnNullThrows()
	{
		IEnumerable? data = null;
		void action() => Guard.Argument(data, nameof(data)).Empty();

		Assert.Throws<ArgumentNullException>(action);
	}

	[Theory]
	[InlineData(new[] { 1 }, false)]
	[InlineData(new int[] { }, true)]
	public void NotEmpty(int[] data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).NotEmpty();

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Theory]
	[InlineData(new object[] { 1 }, false)]
	[InlineData(new object[] { }, true)]
	public void NotEmptyOnNotCollection(object[] data, bool shouldThrow)
	{
		NotCollectionEnumerable enumerable = new(data);
		void action() => Guard.Argument(enumerable, nameof(enumerable)).NotEmpty();

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Fact]
	public void NotEmptyOnNullThrows()
	{
		IEnumerable? data = null;
		void action() => Guard.Argument(data, nameof(data)).NotEmpty();

		Assert.Throws<ArgumentNullException>(action);
	}

	[Theory]
	[InlineData(new[] { 1, 2, 3 }, false)]
	[InlineData(new[] { 1 }, true)]
	[InlineData(null, true)]
	public void ElementsCount(int[]? data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).ElementsCount(x => x.GreaterThan(2));

		if (shouldThrow)
		{
			if (data == null)
				Assert.Throws<ArgumentNullException>(action);
			else
				Assert.Throws<ArgumentException>(action);
		}
		else
			action();
	}

	[Theory]
	[InlineData(new object[] { 1, 2, 3 }, false)]
	[InlineData(new object[] { 1 }, true)]
	public void ElementsCountOnNotCollection(object[] data, bool shouldThrow)
	{
		NotCollectionEnumerable enumerable = new(data);
		void action() => Guard.Argument(enumerable, nameof(enumerable)).ElementsCount(x => x.GreaterThan(2));

		if (shouldThrow)
			Assert.Throws<ArgumentException>(action);
		else
			action();
	}

	[Theory]
	[InlineData(new[] { 1, 2, 3 }, true)]
	[InlineData(new[] { 1 }, false)]
	[InlineData(null, true)]
	public void AllElements(int[]? data, bool shouldThrow)
	{
		void action() => Guard.Argument(data, nameof(data)).AllElements(x => x.LessThanOrEqualTo(2));

		if (shouldThrow)
		{
			if (data == null)
				Assert.Throws<ArgumentNullException>(action);
			else
				Assert.Throws<AggregateException>(action);
		}
		else
			action();
	}

	private class NotCollectionEnumerable : IEnumerable
	{
		private readonly object[] _objs;

		public NotCollectionEnumerable(object[] objs)
		{
			_objs = objs;
		}

		public IEnumerator GetEnumerator() => _objs.GetEnumerator();
	}
}
