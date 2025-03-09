namespace UserService.Application.Responses.CommandsResponses
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public CreateUserResponse(Guid id)
        {
            Id = id;
        }
    }
}
