using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShopWebAPI.DAL;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;
using OnlineShopWebAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShopWebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IConfigurationSection _secretKey;

        public AuthenticationService(IRepositoryWrapper repository, IMapper mapper, IConfiguration config)
        {
            this.repository = repository;
            this.mapper = mapper;
            _secretKey = config.GetSection("SecretKey");
        }

        public string Login(LoginDto loginDTO)
        {
            if (!repository.Users.GetItems().Any())
            {
                return null;
            }

            User user = repository.Users.GetItems().Where(x => x.Email == loginDTO.Email && x.LoginType == false).FirstOrDefault();
            if (user == null)
            {
                return string.Empty;
            }
            if (user.AccountType == AccountType.Seller && (user.AccountStatus == AccountStatus.Created || user.AccountStatus == AccountStatus.Blocked))
            {
                return "User not verified";
            }

            if (BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                List<Claim> claims = new List<Claim>();
                if (user.AccountType == AccountType.Admin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                if (user.AccountType == AccountType.Shopper)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "shopper"));
                }
                if (user.AccountType == AccountType.Seller)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "seller"));
                }
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44398",
                    claims: claims, //claimovi
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            }
            else
            {
                return string.Empty;
            }

        }

        public void Register(UserDto accountDTO)
        {
            if (!repository.Users.GetItems().Any())
            {

                User newUser = mapper.Map<User>(accountDTO);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
                newUser.Id = Guid.NewGuid().ToString();
                newUser.LoginType = false;
                if (newUser.AccountType == AccountType.Shopper)
                {
                    newUser.AccountStatus = AccountStatus.Verified;
                }

                repository.Users.AddItem(newUser);
                repository.Save();
                return;
            }

            User user = repository.Users.GetItems().Where(x => x.Email == accountDTO.Email).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("This email belongs to a different account!");
            }
            else
            {
                User newUser = mapper.Map<User>(accountDTO);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
                newUser.Id = Guid.NewGuid().ToString();
                newUser.LoginType = false;
                if (newUser.AccountType == AccountType.Shopper)
                {
                    newUser.AccountStatus = AccountStatus.Verified;
                }

                repository.Users.AddItem(newUser);
                repository.Save();

                return;

            }
        }

        public string SocialLogin(UserDto accountDTO)
        {
            User user = repository.Users.GetItems().Where(x => x.Email == accountDTO.Email && x.LoginType == true).FirstOrDefault();

            if (user == null)
            {
                User newUser = mapper.Map<User>(accountDTO);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Username); // GMAIL ID stavljen u username
                newUser.Id = Guid.NewGuid().ToString();
                newUser.LoginType = true;
                newUser.AccountStatus = AccountStatus.Verified;
                newUser.AccountType = AccountType.Shopper;
                newUser.DateOfBirth = DateTime.Now;
                newUser.Username = newUser.Email;
                repository.Users.AddItem(newUser);
                repository.Save();

                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Role, "shopper") };
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44398",
                    claims: claims, //claimovi
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            else
            {

                if (BCrypt.Net.BCrypt.Verify(accountDTO.Username, user.Password))
                {

                    List<Claim> claims = new List<Claim>();
                    if (user.AccountType == AccountType.Admin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    if (user.AccountType == AccountType.Shopper)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "shopper"));
                    }
                    if (user.AccountType == AccountType.Seller)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "seller"));
                    }
                    SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:44398",
                        claims: claims, //claimovi
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signinCredentials
                    );
                    return new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
