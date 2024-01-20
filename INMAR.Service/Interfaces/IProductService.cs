using INMAR.Service.Models;

namespace INMAR.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> InsertOrUpdateProduct(Product product);
        Task<bool> DeleteProduct(long productId);
        Task<IQueryable<Product>> GetAllProduct();
        Task<Product> GetProduct(long productId);
    }
}
