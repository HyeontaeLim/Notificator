using static Notificator.Models.ReturnCodeModel;
using static Notificator.Models.UsersModel;

namespace Notificator.Repository;

public interface IUserRepository
{
    public List<User> FindAllUsers();
    public List<User> FindUsersByName(string name);
    public User? FindUsersById(int id);
    public User? FindUsersByUserId(string userId);
    public ReturnRs PutUser(UsersRq usersRq);
}