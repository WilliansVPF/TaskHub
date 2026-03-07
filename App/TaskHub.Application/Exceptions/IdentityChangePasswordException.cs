using Microsoft.AspNetCore.Identity;

namespace TaskHub.Application.Exceptions;

public class IdentityChangePasswordException : Exception
{
    public IEnumerable<string> Errors { get; }

    public IdentityChangePasswordException(IEnumerable<IdentityError> identityErrors)
    {
        Errors = identityErrors.Select(e => e.Description).ToList();
    }
}