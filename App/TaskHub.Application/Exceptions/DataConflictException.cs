namespace TaskHub.Application.Exceptions;

public class DataConflictException : Exception
{
    public DataConflictException(string errorMessage) : base(errorMessage)
    {
    }
}