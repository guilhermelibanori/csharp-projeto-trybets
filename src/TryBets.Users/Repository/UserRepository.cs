using TryBets.Users.Models;
using TryBets.Users.DTO;

namespace TryBets.Users.Repository;

public class UserRepository : IUserRepository
{
    protected readonly ITryBetsContext _context;
    public UserRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public User Post(User user)
    {
        try
        {
            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                throw new Exception("E-mail already used");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    public User Login(AuthDTORequest login)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                throw new Exception("Authentication failed");
            }
            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}