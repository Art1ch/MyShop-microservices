using MediatR;
using OneOf;
using Shared.Results;

namespace UserService.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<OneOf<Success, Failed>>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
