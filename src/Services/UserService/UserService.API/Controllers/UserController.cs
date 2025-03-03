using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.CreateUser;
using UserService.Application.Commands.DeleteUser;
using UserService.Application.Commands.UpdateUser;
using UserService.Application.Queries.GetAll;
using UserService.Application.Queries.GetUserById;
using UserService.Application.Queries.GetUserByName;
using UserService.Core.Entities;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/users")]
        public async Task<ActionResult<IReadOnlyList<UserEntity>>> GetAllUsers()
        {
            var users = await _sender.Send(new GetAllQuery());
            return Ok(users);
        }

        [HttpGet("/userbyid/{id}")]
        public async Task<ActionResult<UserEntity>> GetUserById(Guid id)
        {
            UserEntity? user = await _sender.Send(new GetUserByIdQuery(id));

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("/userbyname/{name}")]
        public async Task<ActionResult<UserEntity>> GetUserByName(string name)
        {
            UserEntity? user = await _sender.Send(new GetUserByNameQuery(name));

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command)
        {
            Guid result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<Guid>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            Guid result = await _sender.Send(command);
            return Ok(result)
;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
