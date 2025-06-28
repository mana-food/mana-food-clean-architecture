using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetBy(c => c.Id == request.Id && !c.Deleted, cancellationToken);
        
        if (user == null)
            throw new ArgumentException($"Usuário com ID {request.Id} não encontrado");


        user.Name = request.Name;
        user.Email = request.Email;
        user.Cpf = request.Cpf;
        user.Birthday = request.Birthday;
        user.UserType = (UserType)request.UserType;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(user, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
