using UserService.Core.Entities;

namespace UserService.Application.Responses.QueriesResponses
{
    public class GetAllResponse
    {
        public List<UserEntity> Users { get; set; }
        public GetAllResponse(List<UserEntity> users)
        {
            Users = users;
        }
    }
}
