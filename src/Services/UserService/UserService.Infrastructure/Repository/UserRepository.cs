using Microsoft.EntityFrameworkCore;
using OneOf;
using Shared.Results;
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

        public async Task<OneOf<Success<Guid>, Failed>> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Success<Guid>(user.Id);
        }

        public async Task<OneOf<Success, Failed>> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Success();
        }

        public async Task<OneOf<Success<List<UserEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
            return new Success<List<UserEntity>>(users);
        }

        public async Task<OneOf<Success<UserEntity>, Failed>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null) { return new Failed("User is null"); }
            return new Success<UserEntity>(user);
        }

        public async Task<OneOf<Success<UserEntity>, Failed>> GetUserByNameAsync(string name, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
            if (user == null) { return new Failed("User is null"); }
            return new Success<UserEntity>(user);
        }

        public async Task<OneOf<Success<Guid>, Failed>> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
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

            return new Success<Guid>(user.Id);
        }
    }
}
