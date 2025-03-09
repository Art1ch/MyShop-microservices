namespace UserService.Application.Responses.CommandsResponses
{
    public class UpdateUserResponse
    {
        public Guid Id { get; set; }
        public UpdateUserResponse(Guid id)
        {
            Id = id;
        }
    }
}
