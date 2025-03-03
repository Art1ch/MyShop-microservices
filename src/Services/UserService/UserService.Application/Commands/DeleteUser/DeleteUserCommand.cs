using MediatR;

namespace UserService.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
