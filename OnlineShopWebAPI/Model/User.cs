namespace OnlineShopWebAPI.Model
{
    public enum AccountStatus
    {
        Created,
        Verified,
        Blocked,
    }

    public enum AccountType
    {
        Admin,
        Shopper,
        Seller
    }

    public class User
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string AccountImage { get; set; } = "";
        public string Address { get; set; } = "";
        public bool LoginType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountType AccountType { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
