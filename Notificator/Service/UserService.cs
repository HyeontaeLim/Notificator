using Microsoft.AspNetCore.Identity;
using Notificator.Models;
using Notificator.Repository;
using static Notificator.Models.UsersModel;

namespace Notificator.Service;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<string> _passwordHasher;

    public UserService(IUserRepository userRepository, PasswordHasher<string> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public List<User> FindAllUsers()
    {
        return _userRepository.FindAllUsers();
    }

    public List<User> FindUsersByName(string name)
    {
        return _userRepository.FindUsersByName(name);
    }

    public User? FindUsersById(int id)
    {
        return _userRepository.FindUsersById(id);
    }

    public User? FindUsersByUserId(string userId)
    {
        return _userRepository.FindUsersByUserId(userId);
    }

    public ReturnCodeModel.ReturnRs PutUser(UsersRq usersRq)
    {
        return _userRepository.PutUser(usersRq);
    }
    
    public User? LoginUser(LoginRq loginRq)
    {
        User? loginUser = _userRepository.FindUsersByUserId(loginRq.UserId);
        return loginUser != null &&
            _passwordHasher.VerifyHashedPassword(loginRq.UserId, loginUser.Password, loginRq.Password) ==
            PasswordVerificationResult.Success
                ? loginUser
                : null;
    }
}