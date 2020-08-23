namespace Microservices.Orders.Core.Entities.Base
{
    public interface IBaseEntity<T_Id>
    {
        T_Id Id { get; }
    }
}