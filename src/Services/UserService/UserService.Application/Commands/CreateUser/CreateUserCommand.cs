using MediatR;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
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
