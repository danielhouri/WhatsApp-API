namespace API.Services
{
    public interface IUserService
    {

        public User? SignIn(string Username, string Password);
        public bool SignUp(string Username, string Name, string Password, string image);
    }
}
