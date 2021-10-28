namespace AdnFabioRamos.Domain.Entities
{
    public class EntityBase<T>: DomainEntity, IEntityBase<T>
    {
        public virtual T Id { get; set; } = default!;
    }

    public class DomainEntity {

    }
}