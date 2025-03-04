using MediatR;
using OneOf;
using Shared.Results;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<OneOf<Success<Guid>, Failed>>
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
