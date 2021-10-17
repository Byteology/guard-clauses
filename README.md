[![NuGet](https://img.shields.io/nuget/v/Byteology.GuardClauses.svg)](https://www.nuget.org/packages/Byteology.GuardClauses)

# Byteology's Guard Clauses

A simple extensible package containing generic guard clause extensions.

A [guard clause](https://deviq.com/design-patterns/guard-clause) is a software pattern that simplifies complex functions by "failing fast", checking for invalid inputs up front and immediately failing if any are found.

## Usage

```c#
public void DepositBook(Book book)
{
    Guard.Argument(book, nameof(book)).NotNull();

    // Deposit the book
}

// OR

public class Book
{
    public string Name { get; }
    public string Author { get; }
    public string[] Genres { get; }
    public int NumberOfPages { get; }
    public DateTime PublicationDate { get; }

    public Book(string name, string author, string[] genres, int numberOfPages, DateTime publicationDate)
    {
        Guard.Argument(name, nameof(name)).NotNullOrWhiteSpace();
        Guard.Argument(author, nameof(author)).NotNullOrWhiteSpace();
        Guard.Argument(genres, nameof(genres))
            .NotNull()
            .ElementsCount(c => c.GreaterThan(0));
        Guard.Argument(numberOfPages, nameof(numberOfPages)).GreaterThan(0);
        Guard.Argument(publicationDate, nameof(publicationDate)).NotDefault();

        // Initialize the book
    }
}
```

## Guard Clause Chaining

In order to make the guard code as readable as possible you are able to chain as many guard clauses as you like:

```c#
    Guard.Argument(genres, nameof(genres))
        .NotNull()
        .ElementsCount(c => c.GreaterThan(0));
```
## Generics

Guard clauses are generic so only applicable to the parameter type will show in the IntelliSense. Additionally guard clauses with parameters will have their types infered.

## Supported Guard Clauses

The library supports the following guard clauses out of the box.

### For all parameter types

- **NotNull()** - throw if the argument is null.
- **NotDefault()** - throws if the argument is equal to its default value.
- **EqualsTo(value)** - throws if the argument does not equal to the specified value.
- **NotEqualsTo(value)** - throws if the argument is equal to the specified value.
- **Satisfies(predicate)** - throws if the argument does not satisfy the specified predicate.

### For string parameters

- **NotNullOrEmpty()** - throws if the argument is null or empty.
- **NotNullOrWhiteSpace()** - throws if the argument is null or empty or consists of only white-space characters.
- **InFormat(regexPattern, options)** - throws if the argument is not in the format specified by a regular expression.

### For IComparable parameters

- **GreaterThan(value)** - throws if the argument is not greater than the specified value.
- **GreaterThanOrEqualTo(value)** - throws if the argument is not greater than or equal to the specified value.
- **LessThan(value)** - throws if the argument is not less than the specified value.
- **LessThanOrEqualTo(value)** - throws if the argument is not less than or equal to the specified value.
- **InRange(min, max)** - throws if the argument is not within the specified range.
- **NotInRange(min, max)** - throws if the argument is within the specified range.

### For IEnumerable parameters

- **ElementsCount(guardClause)** - throws if the number of elements in the argument does not pass the specified guard clause.
- **AllElements(guardClause)** - throws if at least one element of the argument does not pass the specified guard clause.

## Extending with your own Guard Clauses

To extend your own guards, you can do the following:

```c#
// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Byteology.GuardClauses
{
    public static class GuardClauseExtensions
    {
        public static IGuardClause<Book> NotBlacklisted<Book>(this IGuardClause<Book> clause)
        {
            if (clause.Argument.Author == "Blacklisted Author")
                throw new ArgumentException($"{clause.ArgumentName} is in the list of blacklisted books.");

            return clause;
        }
    }
}

// Usage
public void DepositBook(Book book)
{
    Guard.Argument(book, nameof(book))
        .NotNull()
        .NotBlacklisted();

    // Deposit the book
}
```
