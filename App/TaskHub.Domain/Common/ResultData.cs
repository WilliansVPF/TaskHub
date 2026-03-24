using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Common;

public class ResultData<T> : Result
{
    public T? Data { get; }

    protected ResultData(T? data, bool isSuccess, string? message, ResultStatus status, IEnumerable<string>? errors = null) : base(isSuccess, message, status, errors)
    {
        Data = data;
    }

    public static ResultData<T> Success(T data, ResultStatus status = ResultStatus.Ok) => new(data, true, null, status);

    public new static ResultData<T> Failure(string message, ResultStatus status = ResultStatus.BadRequest, IEnumerable<string>? errors = null) => new(default, false, message, status, errors);
}