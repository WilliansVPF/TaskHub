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

    public Result PodeVer(Tarefa tarefa, bool ehMembro, string userId)
    {
        if (tarefa.IdProjeto is null)
        {
            if (tarefa.IdUsuario != userId) return Result.Failure("Tarefa não econtrada", ResultStatus.NotFound);
        }
        else
        {
            if (!ehMembro) return Result.Failure("Tarefa não encontrada", ResultStatus.NotFound);
        }

        return Result.Success();
    }

    public Result PodeEditar(Tarefa tarefa, MembroProjeto? membro, string userId)
    {
        if (tarefa.IdProjeto is null)
        {
            if (tarefa.IdUsuario != userId) return Result.Failure("Tarefa não econtrada", ResultStatus.NotFound);
        }
        else
        {
            if (membro is null) return Result.Failure("Tarefa não econtrada", ResultStatus.NotFound);

            if (membro.Privilegio != Privilegio.Dono && membro.Privilegio != Privilegio.Admin && tarefa.IdUsuario != userId) 
                return Result.Failure("Usuário não tem autorização para editar essa tarefa", ResultStatus.Forbidden); 
        }

        return Result.Success();
    }

    public Result PodeAdicionarResponsavel(Tarefa tarefa, string userId, bool userEhMembro, bool responsavelEhMembro, bool ehResponsavel)
    {
        if (tarefa.IdProjeto is null)
        {
            if (tarefa.IdUsuario != userId) return Result.Failure("Tarefa não encontrada", ResultStatus.NotFound);
            return Result.Failure("Tarefa avulsa não precisa de responsável", ResultStatus.Conflict);
        }
        else
        {
            if (!userEhMembro) return Result.Failure("Tarefa não encontrada", ResultStatus.NotFound);

            if (!responsavelEhMembro) return Result.Failure("Usuário responsável não é membro do projeto", ResultStatus.Conflict);

            if (ehResponsavel) return Result.Failure("Usuário já é responsável por essa tarefa", ResultStatus.Conflict);
        }

        return Result.Success(ResultStatus.NoContent);
    }
}