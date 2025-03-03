using UserService.Application.Commands.CreateUser;
using UserService.Application.Commands.DeleteUser;
using UserService.Application.Commands.UpdateUser;
using UserService.Core.Entities;

namespace UserService.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByNameAsync(string name, CancellationToken cancellationToken);
    }
}
