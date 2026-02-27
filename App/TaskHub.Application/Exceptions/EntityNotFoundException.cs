namespace TaskHub.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string errorMessage) : base(errorMessage)
    {
        
    }
}