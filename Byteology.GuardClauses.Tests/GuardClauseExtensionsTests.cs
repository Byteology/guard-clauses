using System;
using System.Collections.Generic;
using Xunit;

namespace Byteology.GuardClauses.Tests
{
    public class GuardClauseExtensionsTests
    {
        #region Generic

        [Theory]
        [MemberData(nameof(NotNullData))]
        public void NotNull(object obj, bool shouldThrow)
        {
            void action() => Guard.Argument(obj, nameof(obj)).NotNull();

            if (shouldThrow)
                Assert.Throws<ArgumentNullException>(action);
            else
                action();
        }
        public static IEnumerable<object[]> NotNullData() => new object[][] 
            { 
                new object[] { null, true},
                new object[] { 0, false },
                new object[] { 5, false },
                new object[] { "", false },
                new object[] { new int[] { }, false },
                new object[] { default(DateTimeOffset), false },
                new object[] { default(DateTime), false },
            };

        [Theory]
        [MemberData(nameof(NotDefaultData))]
        public void NotDefault(object obj, bool shouldThrow)
        {
            void action() => Guard.Argument(obj, nameof(obj)).NotDefault();

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }
        public static IEnumerable<object[]> NotDefaultData() => new object[][]
            {
                new object[] { null, true},
                new object[] { 0, true },
                new object[] { 5, false },
                new object[] { "", false },
                new object[] { new int[] { }, false },
                new object[] { default(DateTimeOffset), true },
                new object[] { default(DateTime), true },
            };

        [Theory]
        [InlineData(5, 5, false)]
        [InlineData(0, 5, true)]
        [InlineData(0, null, true)]
        [InlineData(null, 5, true)]
        [InlineData(null, null, false)]
        public void EqualsTo(object argument, object other, bool shouldThrow)
        {
            void action() => Guard.Argument(argument, nameof(argument)).EqualsTo(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(5, 5, true)]
        [InlineData(0, 5, false)]
        [InlineData(0, null, false)]
        [InlineData(null, 5, false)]
        [InlineData(null, null, true)]
        public void NotEqualsTo(object argument, object other, bool shouldThrow)
        {
            void action() => Guard.Argument(argument, nameof(argument)).NotEqualsTo(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(5, false)]
        [InlineData(-5, true)]
        public void Satisfies(int data, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).Satisfies(x => x > 0);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        #endregion

        #region String

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
        [InlineData("aaab", "a+b", false)]
        [InlineData("aaab", "a+", true)]
        [InlineData("", "a+", true)]
        [InlineData(null, "a+", true)]
        public void InFormat(string data, string pattern, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).InFormat(pattern);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        #endregion

        #region Comparing

        [Theory]
        [InlineData(1, 3, true)]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void GreaterThan(int data, int other, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).GreaterThan(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(1, 3, true)]
        [InlineData(1, 1, false)]
        [InlineData(1, 0, false)]
        public void GreaterThanOrEqualTo(int data, int other, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).GreaterThanOrEqualTo(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(1, 3, false)]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, true)]
        public void LessThan(int data, int other, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).LessThan(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(1, 3, false)]
        [InlineData(1, 1, false)]
        [InlineData(1, 0, true)]
        public void LessThanOrEqualTo(int data, int other, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).LessThanOrEqualTo(other);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(3, 1, 5, false)]
        [InlineData(3, 3, 5, false)]
        [InlineData(3, 1, 3, false)]
        [InlineData(3, 5, 7, true)]
        [InlineData(3, 1, 2, true)]
        public void InRange(int data, int min, int max, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).InRange(min, max);

            if (shouldThrow)
                Assert.Throws<ArgumentOutOfRangeException>(action);
            else
                action();
        }

        [Theory]
        [InlineData(3, 1, 5, true)]
        [InlineData(3, 3, 5, true)]
        [InlineData(3, 1, 3, true)]
        [InlineData(3, 5, 7, false)]
        [InlineData(3, 1, 2, false)]
        public void NotInRange(int data, int min, int max, bool shouldThrow)
        {
            void action() => Guard.Argument(data, nameof(data)).NotInRange(min, max);

            if (shouldThrow)
                Assert.Throws<ArgumentOutOfRangeException>(action);
            else
                action();
        }

        #endregion

        #region Enumerables

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

        #endregion
    }
}
