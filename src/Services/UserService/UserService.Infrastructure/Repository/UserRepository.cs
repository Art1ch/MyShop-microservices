using Microsoft.EntityFrameworkCore;
using UserService.Application.Commands.CreateUser;
using UserService.Application.Commands.DeleteUser;
using UserService.Application.Commands.UpdateUser;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<UserEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<UserEntity?> GetUserByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync
            (
            s => s.SetProperty(u => u.Name, user.Name)
                  .SetProperty(u => u.Age, user.Age)
                  .SetProperty(u => u.ModifiedTime, DateTime.UtcNow),
            cancellationToken
            );

            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
