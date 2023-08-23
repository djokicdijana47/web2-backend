using OnlineShopWebAPI.DataTransferObject;

namespace OnlineShopWebAPI.Interface
{
    public interface IUserService
    {
        void VerifySeller(string sellerId);
        List<UserDto> GetSellers();
        List<UserDto> GetShoppers();
        UserDto UpdateAccount(UserDto accountLoginDto);
        bool IsSellerVerified(string email);
        string UploadImage(IFormFile imageFile, string email);
        void BlockSeller(string sellerId);
        UserDto GetUserProfile(string email);
    }
}
