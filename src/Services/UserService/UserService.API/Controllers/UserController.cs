using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.CreateUser;
using UserService.Application.Commands.DeleteUser;
using UserService.Application.Commands.UpdateUser;
using UserService.Application.Queries.GetAll;
using UserService.Application.Queries.GetUserById;
using UserService.Application.Queries.GetUserByName;
using UserService.Application.Responses.CommandsResponses;
using UserService.Application.Responses.QueriesResponses;

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
        public async Task<ActionResult<GetAllResponse>> GetAllUsers()
        {
            var result = await _sender.Send(new GetAllQuery());
            return result.Match<ActionResult<GetAllResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/userbyid/{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById([FromQuery] Guid id)
        {
            var result = await _sender.Send(new GetUserByIdQuery(id));
            return result.Match<ActionResult<GetUserByIdResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/userbyname/{name}")]
        public async Task<ActionResult<GetUserByNameResponse>> GetUserByName([FromQuery] string name)
        {
            var result = await _sender.Send(new GetUserByNameQuery(name));
            return result.Match<ActionResult<GetUserByNameResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }
        
        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<CreateUserResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPatch]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<UpdateUserResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );  
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<DeleteUserResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }
    }
}
