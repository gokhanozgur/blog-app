using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Users;

namespace UserService.Persistence.Contexts;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}