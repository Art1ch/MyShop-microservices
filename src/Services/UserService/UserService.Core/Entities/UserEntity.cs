using Shared.Entities;

namespace UserService.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
