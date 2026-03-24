using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public ResultStatus Status { get; set; }
    public DateTime Timestamp { get; set; }
    public IEnumerable<string> Errors { get; }

    protected Result(bool isSuccess, string? message, ResultStatus status, IEnumerable<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Status = status;
        Timestamp = DateTime.Now;
        Errors = errors ?? Enumerable.Empty<string>();
    }

    public static Result Success(ResultStatus status = ResultStatus.NoContent) => new (true, null, status);

    public static Result Failure(string message, ResultStatus status = ResultStatus.BadRequest, IEnumerable<string>? errors = null) => new(false, message, status, errors);
}