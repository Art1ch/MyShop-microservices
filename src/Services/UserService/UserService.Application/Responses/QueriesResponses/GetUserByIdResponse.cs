using UserService.Core.Entities;

namespace UserService.Application.Responses.QueriesResponses
{
    public class GetUserByIdResponse
    {
        public UserEntity User { get; set; }
        public GetUserByIdResponse(UserEntity user)
        {
            User = user;
        }
    }
}
