using INMAR.Service.DdContextConfiguration;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;

namespace INMAR.Service.Repository
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext dbContext;

        public ProductService(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> DeleteProduct(long productId)
        {
            var product = await dbContext.products.FindAsync(productId);

            if (product == null)
                return false;

            dbContext.products.Remove(product);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IQueryable<Product>> GetAllProduct()
        {
            var products = dbContext.products;
            return products;
        }

        public async Task<Product> GetProduct(long productId)
        {
            var product = await dbContext.products.FindAsync(productId);
            return product;
        }

        public async Task<bool> InsertOrUpdateProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                dbContext.products.Add(product);
            }
            else
            {
                var existingProduct = await dbContext.products.FindAsync(product.ProductId);
                if (existingProduct == null)
                    return false;
                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.Price = product.Price;
                existingProduct.InStock = product.InStock;
                existingProduct.ModifiedOn = DateTimeOffset.UtcNow;
                existingProduct.ModifiedBy = product.ModifiedBy;
                existingProduct.IsActive = product.IsActive;
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
        
    }

}
