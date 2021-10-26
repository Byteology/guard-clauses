using System;
using Xunit;

namespace Byteology.GuardClauses.Tests
{
    public class ComparableExtensionsTests
    {
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
    }
}
