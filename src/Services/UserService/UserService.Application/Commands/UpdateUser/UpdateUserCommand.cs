using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.CommandsResponses;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<OneOf<Success<UpdateUserResponse>, Failed>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password  { get; set; }

        public UpdateUserCommand(Guid id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}
