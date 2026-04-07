using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Domain.DomainServices;

public class TarefaDomainService
{
    public Result PodeExcluir(Tarefa tarefa, ApplicationUser user, MembroProjeto? membro)
    {
        if (tarefa.IdProjeto is null)
        {
            if (tarefa.IdUsuario != user.Id) return Result.Failure("Esta tarefa não pertence ao usuário!", ResultStatus.Forbidden);
        }
        else
        {
            if (membro is null) return Result.Failure("O usuário não pertence ao projeto", ResultStatus.Forbidden);

            if (membro.Privilegio != Privilegio.Dono && membro.Privilegio != Privilegio.Admin) return Result.Failure("O usuário não possui permissão para excluir a tarefa");
        }

        return Result.Success(ResultStatus.NoContent);
    }
}