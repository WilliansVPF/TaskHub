namespace TaskHub.Domain.Enums;

public enum ResultStatus
{
    Ok,
    Created,
    NoContent,
    BadRequest,
    Unauthorized,
    Forbidden,
    NotFound,
    Conflict,
    InternalError
}