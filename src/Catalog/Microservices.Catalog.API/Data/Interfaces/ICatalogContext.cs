using Microservices.Catalog.API.Entities;
using MongoDB.Driver;

namespace Microservices.Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}