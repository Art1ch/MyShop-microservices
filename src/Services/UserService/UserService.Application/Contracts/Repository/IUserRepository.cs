using OneOf;
using Shared.Results;
using UserService.Core.Entities;

namespace UserService.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<OneOf<Success, Failed>> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Success<Guid>, Failed>> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<OneOf<Success<List<UserEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken);
        Task<OneOf<Success<UserEntity>, Failed>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Success<UserEntity>, Failed>> GetUserByNameAsync(string name, CancellationToken cancellationToken);
    }
}
