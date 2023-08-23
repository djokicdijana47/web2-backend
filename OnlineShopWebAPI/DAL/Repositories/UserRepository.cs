using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private OnlineShopDbContext dbContext;
        public UserRepository(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddItem(User item)
        {
            dbContext.UserAccounts.Add(item);

        }

        public User GetItemById(string id)
        {
            return dbContext.UserAccounts.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetItems()
        {
            return dbContext.UserAccounts.ToList();
        }

        public bool ItemExists(string id)
        {
            return dbContext.UserAccounts.First(x => x.Id == id) != null;
        }

        public User ModifyItem(User item)
        {
            var user = dbContext.UserAccounts.FirstOrDefault(x => x.Email == item.Email);
            if (user != null)
            {
                user.Address = item.Address;
                user.AccountImage = item.AccountImage;
                user.Name = item.Name;
                user.Lastname = item.Lastname;
                user.DateOfBirth = item.DateOfBirth;
                user.Username = item.Username;
                dbContext.SaveChanges();
                return user;
            }
            return null;
        }

        public void RemoveItem(string id)
        {
            throw new NotImplementedException();
        }
    }
}
