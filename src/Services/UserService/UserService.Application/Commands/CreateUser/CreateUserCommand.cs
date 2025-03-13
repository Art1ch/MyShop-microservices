using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.CommandsResponses;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<OneOf<Success<CreateUserResponse>, Failed>>
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
