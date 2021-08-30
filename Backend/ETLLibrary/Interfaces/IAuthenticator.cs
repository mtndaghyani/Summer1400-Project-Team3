using ETLLibrary.Database;

namespace ETLLibrary.Interfaces
{
    public interface IAuthenticator
    {
        User ValidateUser(string username, string password);
        string Login(User user);
        public void Logout(string token);
    }
}