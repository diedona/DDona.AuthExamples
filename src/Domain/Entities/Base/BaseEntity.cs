namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        protected readonly List<string> _Errors = new List<string>();

        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        public bool HasErrors => _Errors.Any();

        public IReadOnlyList<string> GetErrors() => _Errors.AsReadOnly();

    }
}
