namespace Byteology.GuardClauses
{
    /// <summary>
    /// An entry point to a set of extension methods on <see cref="IGuardClause{T}"/>.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Sets an argument that should be guarded.
        /// </summary>
        /// <typeparam name="T">The type of the argument to be guarded</typeparam>
        /// <param name="argument">The argument to be guarded</param>
        /// <param name="argumentName">The name of the argument to be guarded.</param>
        public static IGuardClause<T> Argument<T>(T argument, string argumentName) 
            => new GuardClause<T>(argument, argumentName);
    }
}
