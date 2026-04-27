using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Domain.DomainServices;

public class UserDomainService
{
    private readonly UserManager<ApplicationUser> _userMaganer;

    public UserDomainService(UserManager<ApplicationUser> userMaganer)
    {
        _userMaganer = userMaganer;
    }

    public async Task<Result> VerificaEmail(string email, string? userId = null)
    {
        var emailExist = await _userMaganer.Users.AnyAsync(u => u.Email == email && u.Id != userId);
        if (emailExist) return Result.Failure("Email já cadastrado na base de dados!", ResultStatus.Conflict);
        return Result.Success(ResultStatus.NoContent);
    }
}