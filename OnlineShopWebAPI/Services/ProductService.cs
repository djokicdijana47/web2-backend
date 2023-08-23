using AutoMapper;
using OnlineShopWebAPI.DAL;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public ProductService(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void AddProduct(ProductDto productDto)
        {
            repository.Product.AddItem(mapper.Map<Product>(productDto));
            repository.Save();
        }

        public List<ProductDto> GetProducts()
        {
            return mapper.Map<List<ProductDto>>(repository.Product.GetItems());
        }

        public List<ProductDto> GetSellerProducts(string sellerId)
        {
            return mapper.Map<List<ProductDto>>(repository.Product.GetItems().Where(x => x.Seller == sellerId));
        }

        public void RemoveProduct(ProductDto productDto)
        {
            if (repository.Product.ItemExists(productDto.Id))
            {
                repository.Product.RemoveItem(productDto.Id);
                repository.Save();
            }
        }
    }
}
