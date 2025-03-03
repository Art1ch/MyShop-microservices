using Microsoft.EntityFrameworkCore;
using UserService.Core.Entities;

namespace UserService.Infrastructure.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
