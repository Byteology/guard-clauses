using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Byteology.GuardClauses.Tests
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [InlineData(new int[] { 1 }, true)]
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
        [SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", 
            Justification = "We are testing if the method does not throw an exception.")]
        public void Empty_Null()
        {
            IEnumerable data = null;
            Guard.Argument(data, nameof(data)).Empty();
        }

        [Theory]
        [InlineData(new int[] { 1 }, false)]
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
        public void NotEmpty_NotCollection(object[] data, bool shouldThrow)
        {
            NotCollectionEnumerable enumerable = new(data);
            void action() => Guard.Argument(enumerable, nameof(enumerable)).NotEmpty();

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Fact]
        [SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions",
            Justification = "We are testing if the method does not throw an exception.")]
        public void NotEmpty_Null()
        {
            IEnumerable data = null;
            Guard.Argument(data, nameof(data)).NotEmpty();
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { 1 }, true)]
        public void ElementsCount(int[] data, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).ElementsCount(x => x.GreaterThan(2));

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(new object[] { 1, 2, 3 }, false)]
        [InlineData(new object[] { 1 }, true)]
        public void ElementsCount_NotCollection(object[] data, bool shouldThrow)
        {
            NotCollectionEnumerable enumerable = new(data);
            void action() => Guard.Argument(enumerable, nameof(enumerable)).ElementsCount(x => x.GreaterThan(2));

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Fact]
        [SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions",
            Justification = "We are testing if the method does not throw an exception.")]
        public void ElementsCount_Null()
        {
            IEnumerable data = null;
            Guard.Argument(data, nameof(data)).ElementsCount(x => x.EqualsTo(1));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, true)]
        [InlineData(new int[] { 1 }, false)]
        public void AllElements(int[] data, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).AllElements(x => x.LessThanOrEqualTo(2));

            if (shouldThrow)
                Assert.Throws<AggregateException>(action);
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
}
