namespace TaskHub.Application.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string errorMessage) : base(errorMessage)
    {
    }
}