using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Catalog.API.Data.Interfaces;
using Microservices.Catalog.API.Entities;
using Microservices.Catalog.API.Filters;
using Microservices.Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Microservices.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _ctx;

        public ProductRepository(ICatalogContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Create(Product product)
        {
            await _ctx.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _ctx.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        // public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        // {
        //     var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
        //     return await _ctx.Products.Find(filter).ToListAsync();
        // }

        public async Task<Product> GetProductById(string id)
        {
            return await _ctx.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        // public async Task<IEnumerable<Product>> GetProductByName(string name)
        // {
        //     var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
        //     return await _ctx.Products.Find(filter).ToListAsync();
        // }

        public async Task<IEnumerable<Product>> GetProducts(ProductFilter filter)
        {
            // return await _ctx.Products.Find(p =>
            //     (string.IsNullOrEmpty(filter.Name) || p.Name.ToLower().Contains(filter.Name.ToLower())) &&
            //     (string.IsNullOrEmpty(filter.Category) || p.Category.ToLower().Contains(filter.Category.ToLower()))
            // ).ToListAsync();

            if (!string.IsNullOrEmpty(filter.Name) || !string.IsNullOrEmpty(filter.Category))
            {
                var filterBuilder = Builders<Product>.Filter;
                FilterDefinition<Product> filterDef = null;
                if (!string.IsNullOrEmpty(filter.Name)) filterDef = filterBuilder.Eq(p => p.Name, filter.Name);
                if (!string.IsNullOrEmpty(filter.Category)) filterDef = filterBuilder.Eq(p => p.Category, filter.Category);
                return await _ctx.Products.Find(filterDef).ToListAsync();
            }
            else return await _ctx.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _ctx.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}