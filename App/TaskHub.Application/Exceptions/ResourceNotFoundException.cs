namespace TaskHub.Application.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string errorMessage) : base(errorMessage)
    {
        
    }
}