namespace Microservices.Orders.Core.Entities.Base
{
    public abstract class BaseEntity<T_Id> : IBaseEntity<T_Id>
    {
        public virtual T_Id Id { get; protected set; }

        int? _requestedHashCode;

        public bool IsTransient()
        {
            return Id.Equals(default(T_Id));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity<T_Id>)) return false;
            if (GetType() != obj.GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            var item = (BaseEntity<T_Id>)obj;
            if (IsTransient() || item.IsTransient()) return false;
            else return item == this;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();
            if (_requestedHashCode.HasValue) return _requestedHashCode.Value;
            _requestedHashCode = Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(BaseEntity<T_Id> left, BaseEntity<T_Id> right)
        {
            if (Equals(left, null)) return Equals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<T_Id> left, BaseEntity<T_Id> right)
        {
            if (Equals(left, null)) return Equals(right, null);
            return !(left == right);
        }
    }
}