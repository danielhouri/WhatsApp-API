using API.Data;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly APIContext _context;
        public UserService(APIContext Context)
        {
            _context = Context;
        }

        public User? SignIn(string Username, string Password)
        {
            return _context.User.SingleOrDefault(u => u.Username == Username && u.Password == Password);
        }

        public bool SignUp(string Username, string Name, string Password, string image)
        {
            var res = _context.User.SingleOrDefault(u => u.Username == Username);
            if (res == null)
            {
                var user = new User() { Username = Username, Password = Password, Name = Name, Contacts = new List<Contact>(),  Image=image};
                _context.User.Add(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
