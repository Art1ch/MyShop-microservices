namespace EventBus.Events.User.Create
{
    public class UserCreatedEvent
    {
        public Guid Id { get; set; }
        public UserCreatedEvent(Guid id)
        {
            Id = id;
        }
    }
}
