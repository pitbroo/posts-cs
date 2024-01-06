using Microsoft.EntityFrameworkCore;
using posts_cs.model;

namespace posts_cs.repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Host=localhost;Database=posts;Username=hexagonal;Password=hexagonal")
            .Options;

        _dbContext = new ApplicationDbContext(options);
    }

    public User FindById(int id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }
    public IEnumerable<User> FindAll()
    {
        return _dbContext.Users.ToList();
    }
}
public interface IUserRepository
{
    User FindById(int id);
    IEnumerable<User> FindAll();
}