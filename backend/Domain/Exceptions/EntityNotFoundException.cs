namespace Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public long? EntityId { get; }
        public IEnumerable<long> EntityIds { get; }

        public EntityNotFoundException(string entityName, long? entityId = null)
            : base(GenerateMessageForSingle(entityName, entityId))
        {
            EntityName = entityName;
            EntityId = entityId;
            EntityIds = Enumerable.Empty<long>();
        }

        public EntityNotFoundException(string entityName, IEnumerable<long> entityIds)
            : base(GenerateMessageForMultiple(entityName, entityIds))
        {
            EntityName = entityName;
            EntityIds = entityIds;
        }

        private static string GenerateMessageForSingle(string entityName, long? entityId)
        {
            return entityId == null
                ? $"{entityName} not found."
                : $"{entityName} with ID {entityId} not found.";
        }

        private static string GenerateMessageForMultiple(string entityName, IEnumerable<long> entityIds)
        {
            string ids = string.Join(", ", entityIds);
            return $"{entityName} with IDs [{ids}] not found.";
        }
    }
}
