using RestASPNET.Data.VO;
using RestASPNET.Model;

namespace RestASPNET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string username);
        bool RevokeToken(string username);
        User RefreshUserInfo(User user);
    }
}
