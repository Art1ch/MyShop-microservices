using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.CommandsResponses;

namespace UserService.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<OneOf<Success<DeleteUserResponse>, Failed>>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
