using UserService.Core.Entities;

namespace UserService.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByNameAsync(string name, CancellationToken cancellationToken);
    }
}
