namespace Domain.Exceptions
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException(long id)
        : base($"Tag with ID {id} does not exist")
        {
        }
    }
}
