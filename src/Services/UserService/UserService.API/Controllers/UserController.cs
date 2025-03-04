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
        public async Task<ActionResult<List<UserEntity>>> GetAllUsers()
        {
            var result = await _sender.Send(new GetAllQuery());
            return result.Match<ActionResult<List<UserEntity>>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
                );
        }

        [HttpGet("/userbyid/{id}")]
        public async Task<ActionResult<UserEntity>> GetUserById(Guid id)
        {
            var result = await _sender.Send(new GetUserByIdQuery(id));
            return result.Match<ActionResult<UserEntity>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/userbyname/{name}")]
        public async Task<ActionResult<UserEntity>> GetUserByName(string name)
        {
            var result = await _sender.Send(new GetUserByNameQuery(name));
            return result.Match<ActionResult<UserEntity>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<Guid>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPatch]
        public async Task<ActionResult<Guid>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<Guid>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );  
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult>(
                success => Ok(),
                failed => BadRequest(failed.Message)
            );
        }
    }
}
