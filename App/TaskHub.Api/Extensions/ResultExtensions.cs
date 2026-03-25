using Microsoft.AspNetCore.Mvc;
using TaskHub.Domain.Common;
using TaskHub.Domain.Enums;

namespace TaskHub.Api.Extensions;

public static class ResultExtensions
{
    // public static ActionResult ToActionResult()
    public static ActionResult ToActionResult(this Result result)
    {
        return result.Status switch
        {
          ResultStatus.Ok => new OkObjectResult(result),
          ResultStatus.NoContent => new NoContentResult(),
          ResultStatus.NotFound => new NotFoundObjectResult(result),
          ResultStatus.Conflict => new ConflictObjectResult(result),
          _ => new BadRequestObjectResult(result)
        };
    }
}