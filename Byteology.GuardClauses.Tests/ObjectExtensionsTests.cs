using System;
using System.Collections.Generic;
using Xunit;

namespace Byteology.GuardClauses.Tests
{
    public class ObjectExtensionsTests
    {
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
        [InlineData(5, false, null)]
        [InlineData(5, false, "description")]
        [InlineData(-5, true, null)]
        [InlineData(-5, true, "description")]
        public void Satisfies(int data, bool shouldThrow, string description)
        {
            void action() => Guard.Argument(data, nameof(data)).Satisfies(x => x > 0, description);

            if (shouldThrow)
                Assert.Throws<ArgumentException>(action);
            else
                action();
        }
    }
}
