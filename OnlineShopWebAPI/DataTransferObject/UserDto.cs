using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DataTransferObject
{
    public class UserDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string AccountImage { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountType AccountType { get; set; }
        public bool LoginType { get; set; }
    }
}
