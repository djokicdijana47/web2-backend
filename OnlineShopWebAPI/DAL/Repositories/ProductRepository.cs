using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DAL.Repositories
{
    public class ProductRepository : IRepository<Product> 
    {
        private OnlineShopDbContext dbContext;

        public ProductRepository(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddItem(Product item)
        {
            item.Id = Guid.NewGuid().ToString();
            dbContext.Products.Add(item);
        }

        public Product GetItemById(string id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetItems()
        {
            return dbContext.Products.ToList();
        }

        public bool ItemExists(string id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id) != null;
        }

        public Product ModifyItem(Product item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string id)
        {
            if (dbContext.Products.FirstOrDefault(x => x.Id == id) != null)
            {
                dbContext.Products.Remove(dbContext.Products.FirstOrDefault(x => x.Id == id));
                dbContext.SaveChanges();
            }
        }
    }
}
