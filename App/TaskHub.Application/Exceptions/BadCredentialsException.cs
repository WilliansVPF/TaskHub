namespace TaskHub.Application.Exceptions;

public class BadCredentialsException : Exception
{
    public BadCredentialsException(string erroMessage) : base(erroMessage)
    {
    }
}