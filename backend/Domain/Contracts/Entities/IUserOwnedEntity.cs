namespace Domain.Contracts.Entities
{
    public interface IUserOwnedEntity : IEntity
    {
        public long UserId { get; set; }
    }
}
