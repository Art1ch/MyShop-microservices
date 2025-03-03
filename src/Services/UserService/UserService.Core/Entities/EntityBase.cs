namespace UserService.Core.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
