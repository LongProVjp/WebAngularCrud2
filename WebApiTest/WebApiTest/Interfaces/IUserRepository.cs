using WebApiTest.Model;

namespace WebApiTest.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string Username, string Password);
        bool CreateUser(User User);
        bool UserExit(string Username);
    }
}
