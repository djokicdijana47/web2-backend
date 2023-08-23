using AutoMapper;
using OnlineShopWebAPI.DAL;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;
using OnlineShopWebAPI.Model;
using System.Net.Http.Headers;

namespace OnlineShopWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public UserService(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public void BlockSeller(string sellerId)
        {
            var merchant = repository.Users.GetItems().Where(x => x.Email == sellerId && x.AccountType == AccountType.Seller).FirstOrDefault();
            merchant.AccountStatus = AccountStatus.Blocked;
            repository.Save();
        }

        public List<UserDto> GetSellers()
        {
            return mapper.Map<List<UserDto>>(repository.Users.GetItems().Where(x => x.AccountType == AccountType.Seller).ToList());
        }

        public List<UserDto> GetShoppers()
        {
            return mapper.Map<List<UserDto>>(repository.Users.GetItems().Where(x => x.AccountType == AccountType.Shopper).ToList());
        }

        public UserDto GetUserProfile(string email)
        {
            return mapper.Map<List<UserDto>>(repository.Users.GetItems().Where(x => x.Email == email)).FirstOrDefault();
        }

        public bool IsSellerVerified(string email)
        {
            var user = repository.Users.GetItems().FirstOrDefault(x => x.Email == email);
            if (user.AccountStatus == AccountStatus.Verified)
            {
                return true;
            }
            if (user.AccountStatus == AccountStatus.Blocked)
            {
                return false;
            }
            return false;
        }

        public UserDto UpdateAccount(UserDto UserDto)
        {
            return mapper.Map<UserDto>(repository.Users.ModifyItem(mapper.Map<User>(UserDto)));
        }

        public string UploadImage(IFormFile imageFile, string email)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (imageFile.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(imageFile.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    var us = repository.Users.GetItems().FirstOrDefault(x => x.Email == email);
                    us.AccountImage = dbPath;
                    repository.Users.ModifyItem(us);
                    repository.Save();
                    return dbPath;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public void VerifySeller(string sellerId)
        {
            var merchant = repository.Users.GetItems().Where(x => x.Email == sellerId && x.AccountType == AccountType.Seller).FirstOrDefault();
            merchant.AccountStatus = AccountStatus.Verified;
            repository.Save();
        }
    }
}
