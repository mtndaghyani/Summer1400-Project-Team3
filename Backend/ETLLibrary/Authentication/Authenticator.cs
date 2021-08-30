using System.Collections.Generic;
using System.Linq;
using ETLLibrary.Database;
using ETLLibrary.Interfaces;

namespace ETLLibrary.Authentication
{
    public class Authenticator: IAuthenticator
    {
        private EtlContext _context;
        private Dictionary<string, User> _tokens;

        public Authenticator(EtlContext context)
        {
            _context = context;
            _tokens = new Dictionary<string, User>();
        }

        public User ValidateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        public string Login(User user)
        {
            var token = TokenGenerator.Generate(16);
            _tokens.Add(token, user);
            return token;
        }

        public void Logout(string token)
        {
            _tokens.Remove(token);
        }
    }
}