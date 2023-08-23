using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private OnlineShopDbContext dbContext;
        public OrderRepository(OnlineShopDbContext localDbContext)
        {
            this.dbContext = localDbContext;
        }

        public void AddItem(Order item)
        {
            dbContext.Orders.Add(item);
            
            dbContext.SaveChanges();
        }

        public Order GetItemById(string id)
        {
            return dbContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetItems()
        {
            return dbContext.Orders.Include(x => x.OrderedProducts).ToList();
        }

        public bool ItemExists(string id)
        {
            return dbContext.Orders.FirstOrDefault(x => x.Id == id) != null;
        }

        public Order ModifyItem(Order item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string id)
        {
            if (dbContext.Orders.FirstOrDefault(x => x.Id == id) != null)
            {
                dbContext.Orders.Remove(dbContext.Orders.FirstOrDefault(x => x.Id == id));
                dbContext.SaveChanges();
            }
        }
    }
}
