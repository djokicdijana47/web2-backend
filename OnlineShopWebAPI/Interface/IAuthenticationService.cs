using OnlineShopWebAPI.DataTransferObject;

namespace OnlineShopWebAPI.Interface
{
    public interface IAuthenticationService
    {
        string Login(LoginDto loginDTO);
        void Register(UserDto accountDTO);
        string SocialLogin(UserDto accountDTO);
    }
}
