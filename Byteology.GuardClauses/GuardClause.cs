namespace Byteology.GuardClauses;

internal class GuardClause<T> : IGuardClause<T>
{
	public GuardClause(T? argument, string argumentName)
	{
		Argument = argument;
		ArgumentName = argumentName;
	}

	public T? Argument { get; }
	public string ArgumentName { get; }
}
