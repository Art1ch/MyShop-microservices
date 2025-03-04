using MediatR;
using OneOf;
using Shared.Results;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<OneOf<Success<Guid>, Failed>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }

        public UpdateUserCommand(Guid id, string name, int? age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
