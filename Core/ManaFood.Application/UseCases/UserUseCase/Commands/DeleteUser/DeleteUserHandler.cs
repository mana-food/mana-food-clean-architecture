using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetBy(c => c.Id == request.Id && !c.Deleted, cancellationToken);

        if (user == null)
            throw new ArgumentException($"Categoria com ID {request.Id} não encontrada");


        user.Deleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.Delete(user, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
