using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Catalog.API.Entities;
using Microservices.Catalog.API.Filters;

namespace Microservices.Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(ProductFilter filter);
        Task<Product> GetProductById(string id);
        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}