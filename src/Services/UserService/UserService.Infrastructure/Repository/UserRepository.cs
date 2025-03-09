using Microsoft.EntityFrameworkCore;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Application.Responses.CommandsResponses;
using UserService.Application.Responses.QueriesResponses;
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

        public async Task<OneOf<Success<CreateUserResponse>, Failed>> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var response = new CreateUserResponse(user.Id);
            return new Success<CreateUserResponse>(response);
        }

        public async Task<OneOf<Success<DeleteUserResponse>, Failed>> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var response = new DeleteUserResponse();
            return new Success<DeleteUserResponse>(response);
        }

        public async Task<OneOf<Success<GetAllResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
            var respnose = new GetAllResponse(users);
            return new Success<GetAllResponse>(respnose);
        }

        public async Task<OneOf<Success<GetUserByIdResponse>, Failed>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                return new Failed("User is null");
            }
            var response = new GetUserByIdResponse(user);
            return new Success<GetUserByIdResponse>(response);
        }

        public async Task<OneOf<Success<GetUserByNameResponse>, Failed>> GetUserByNameAsync(string name, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
            if (user == null)
            {
                return new Failed("User is null");
            }
            var respnose = new GetUserByNameResponse(user);
            return new Success<GetUserByNameResponse>(respnose);
        }

        public async Task<OneOf<Success<UpdateUserResponse>, Failed>> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
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

            var response = new UpdateUserResponse(user.Id);
            return new Success<UpdateUserResponse>(response);
        }
    }
}
