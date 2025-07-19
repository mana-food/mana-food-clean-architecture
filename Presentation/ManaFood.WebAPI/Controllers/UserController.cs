using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.UserUseCase.Commands.CreateUser;
using ManaFood.Application.UseCases.UserUseCase.Queries.GetUserById;
using ManaFood.Application.UseCases.UserUseCase.Queries.GetAllUsers;
using ManaFood.Application.UseCases.UserUseCase.Commands.UpdateUser;
using ManaFood.Application.UseCases.UserUseCase.Commands.DeleteUser;
using ManaFood.Application.UseCases.UserUseCase.Queries.GetUserByEmail;
using ManaFood.Application.UseCases.UserUseCase.Queries.GetUserByCpf;
using ManaFood.Domain.Entities;
using ManaFood.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER, UserType.OPERATOR)]
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserByEmailQuery(email), cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER, UserType.OPERATOR)]
        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<UserDto>> GetByCpf(string cpf, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserByCpfQuery(cpf), cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(Guid id, UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        
        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
