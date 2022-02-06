namespace DeliveryApp.Core.Entities.Abstaract
{
    public abstract class EntityBase : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
