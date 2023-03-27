using RestASPNET.Data.VO;
using RestASPNET.Model;
using System.Security.Cryptography;
using RestASPNET.Repository.Generic;
using System.Text;
using RestASPNET.Model.Context;
using System.Data;

namespace RestASPNET.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.UserName.Equals(user.UserName))) return null;

            var result = _context.Users.FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }


        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public User ValidateCredentials(UserVO user)
        {
            
            //Criptografa a senha que chegou do VO
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            //Compara a senha criptografada (pass) com a senha do usuário no banco (que também está criptograda)
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }
        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        private string ComputeHash(string input, SHA256 algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
