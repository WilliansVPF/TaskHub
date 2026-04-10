using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Domain.DomainServices;

public class ProjetoDomainService
{
    public Result PodeAdicionaMembro(MembroProjeto? membroQueAdiciona, MembroProjeto? membroASerAdicionado, Privilegio privilegio)
    {
        if (membroQueAdiciona is null) return Result.Failure("O usuário não pertence ao projeto", ResultStatus.Forbidden);

        if (membroQueAdiciona.Privilegio != Privilegio.Dono
            && membroQueAdiciona.Privilegio != Privilegio.Admin) return Result.Failure("Permissão insuficiente", ResultStatus.Forbidden);

        if (membroASerAdicionado is not null) return Result.Failure("O usuário a ser adicionado já é membro desse projeto", ResultStatus.Conflict);

        if (privilegio == Privilegio.Dono) return Result.Failure("Não é possivel adicionar um novo Dono", ResultStatus.BadRequest);

        return Result.Success();
    }
}