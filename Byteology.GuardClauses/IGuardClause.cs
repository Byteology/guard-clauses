namespace Byteology.GuardClauses
{
    /// <summary>
    /// Provides an extension point for guarding an argument.
    /// </summary>
    /// <typeparam name="T">The type of the guarded argument</typeparam>
    public interface IGuardClause<out T>
    {
        /// <summary>
        /// Gets the guarded argument.
        /// </summary>
        public T Argument { get; }

        /// <summary>
        /// Gets the name of the guarded argument.
        /// </summary>
        public string ArgumentName { get; }
    }
}
