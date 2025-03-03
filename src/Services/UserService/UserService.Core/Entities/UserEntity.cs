using Shared.Entities;

namespace UserService.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public int? Age { get; set; }
    }
}
