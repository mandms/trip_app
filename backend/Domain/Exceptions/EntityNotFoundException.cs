namespace Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public long? EntityId { get; }

        public EntityNotFoundException(string entityName, long? entityId = null)
            : base(entityId == null ? $"{entityName} not found" : $"{entityName} with ID {entityId} not found")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
