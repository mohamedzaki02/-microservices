using Microservices.Catalog.API.Data.Interfaces;
using Microservices.Catalog.API.Entities;
using Microservices.Catalog.API.Settings;
using MongoDB.Driver;

namespace Microservices.Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Products = database.GetCollection<Product>(settings.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}