using posts_cs.model;
using posts_cs.repository;
using WebApi.Authorization;
using WebApi.Models;

namespace posts_cs.Services;

public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model);
    
    IEnumerable<User> GetAll();
    User? GetById(int id);
}

public class UserService : IUserService
{
    /*private List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
        new User { Id = 2, FirstName = "Jan", LastName = "Kowalski", Username = "user", Password = "user" }
    };*/
    
    private readonly IUserRepository _users;

  

    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils, IUserRepository users)
    {
        _jwtUtils = jwtUtils;
        _users = users;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = _users.FindAll().SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
        if (user == null) return null;
        var token = _jwtUtils.GenerateJwtToken(user);
        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _users.FindAll();
    }

    public User? GetById(int id)
    {
        return _users.FindAll().FirstOrDefault(x => x.Id == id);
    }
}