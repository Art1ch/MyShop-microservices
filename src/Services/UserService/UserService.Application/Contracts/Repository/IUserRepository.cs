using OneOf;
using Shared.Results;
using UserService.Application.Responses.CommandsResponses;
using UserService.Application.Responses.QueriesResponses;
using UserService.Core.Entities;

namespace UserService.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<OneOf<Success<CreateUserResponse>, Failed>> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<OneOf<Success<DeleteUserResponse>, Failed>> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Success<UpdateUserResponse>, Failed>> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<OneOf<Success<GetAllResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken);
        Task<OneOf<Success<GetUserByIdResponse>, Failed>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Success<GetUserByNameResponse>, Failed>> GetUserByNameAsync(string name, CancellationToken cancellationToken);
    }
}
