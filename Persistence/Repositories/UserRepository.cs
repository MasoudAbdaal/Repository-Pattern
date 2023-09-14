using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private DbSet<User> Users => Entity;
    public UserRepository(DbContextOptions<ApplicationDbContext<User>> options) : base(options)
    {
    }



    public bool CheckUser() => Users.Any(x => x.UserName == "");
}