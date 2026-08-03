namespace TaskManager.Application.Exceptions;

/// <summary>
/// Any validation failure coming from the domain is wrapped in this ONE type.
/// The UI layer can then catch a single exception instead of many.
/// </summary>
public sealed class InvalidTaskException : Exception
{
    public InvalidTaskException(string message)
        : base(message)
    {
    }

    public InvalidTaskException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
