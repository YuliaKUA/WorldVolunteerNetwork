namespace WorldVolunteerNetwork.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; } = Guid.Empty;
        public static bool operator ==(Entity? a, Entity? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) 
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }
        public override bool Equals(object? obj)
        {
            if(obj is not Entity other)
                return false;

            if(ReferenceEquals(this, other) == false) 
                return false;

            if(GetType() != other.GetType()) 
                return false;
            
            return Id == other.Id;
        }

        public override int GetHashCode() 
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
