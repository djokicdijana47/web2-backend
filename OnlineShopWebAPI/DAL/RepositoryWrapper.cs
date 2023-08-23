using OnlineShopWebAPI.DAL.Repositories;
using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DAL
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private OnlineShopDbContext _dbContext;
        private IRepository<User> _accountRepository;
        private IRepository<Product> _productRepository;
        private IRepository<Order> _orderRepository;

        public RepositoryWrapper(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IRepository<User> Users
        {
            get
            {
                if (_accountRepository == null)
                {
                    return new UserRepository(_dbContext);
                }
                return _accountRepository;
            }
        }
        public IRepository<Product> Product
        {
            get
            {
                if (_productRepository == null)
                {
                    return new ProductRepository(_dbContext);
                }
                return _productRepository;
            }
        }
        public IRepository<Order> Order
        {
            get
            {
                if (_orderRepository == null)
                {
                    return new OrderRepository(_dbContext);
                }
                return _orderRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
