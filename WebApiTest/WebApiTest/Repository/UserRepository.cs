using WebApiTest.Interfaces;
using WebApiTest.Model;

namespace WebApiTest.Repository
{
    public class UserRepository: IUserRepository
    {
    private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
            return true;
        }

        public User GetUser(string Username, string Password)
        {
            var data = _context.Users.Where(u => u.Username == Username && u.Password == Password).FirstOrDefault();
            return data;
        }

        public bool UserExit(string Username)
        {
            var data = _context.Users.Where(u => u.Username == Username).FirstOrDefault() != null;

            return data;
        }
    }
}
