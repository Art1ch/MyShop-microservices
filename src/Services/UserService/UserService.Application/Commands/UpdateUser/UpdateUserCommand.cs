using MediatR;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Guid>
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
