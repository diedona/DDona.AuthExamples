namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
    }
}
