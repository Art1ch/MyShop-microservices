using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.CommandsResponses;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<OneOf<Success<CreateUserResponse>, Failed>>
    {
        public string Name { get; set; }
        public int? Age { get; set; }

        public CreateUserCommand(string name, int? age)
        {
            Name = name;
            Age = age;
        }
    }
}
