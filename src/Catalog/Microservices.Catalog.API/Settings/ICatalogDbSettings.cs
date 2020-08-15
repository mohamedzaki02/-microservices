namespace Microservices.Catalog.API.Settings
{
    public interface ICatalogDbSettings
    {
        string ConnectionString { set; get; }
        string DatabaseName { set; get; }
        string CollectionName { set; get; }
    }
}