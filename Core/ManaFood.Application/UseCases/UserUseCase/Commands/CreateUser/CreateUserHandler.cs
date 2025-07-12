using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserValidationService _userValidationService;

    public CreateUserHandler(IUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper, UserValidationService userValidationService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userValidationService = userValidationService;
    }

    public async Task<UserDto> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {

        var user = _mapper.Map<User>(request);

        await _userValidationService.ValidateUniqueEmailAndCpfAsync(user, cancellationToken);

        await _repository.Create(user, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
