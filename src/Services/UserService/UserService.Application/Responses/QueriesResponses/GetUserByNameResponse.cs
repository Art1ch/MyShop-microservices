using UserService.Core.Entities;

namespace UserService.Application.Responses.QueriesResponses
{
    public class GetUserByNameResponse
    {
        public UserEntity User { get; set; }
        public GetUserByNameResponse(UserEntity user)
        {
            User = user;
        }
    }
}
