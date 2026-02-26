using Microsoft.AspNetCore.Identity;

namespace TaskHub.Application.Exceptions;

public class IdentityCreationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public IdentityCreationException(IEnumerable<IdentityError> identityErrors)
    {
        Errors = identityErrors.Select(e => e.Description).ToList();
    }
}