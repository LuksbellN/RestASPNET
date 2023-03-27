using RestASPNET.Data.VO;

namespace RestASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        public bool RevokeToken(string username);
        TokenVO ValidateCredentials(TokenVO token);
    }
}
