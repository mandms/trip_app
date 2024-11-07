namespace Domain.Exceptions
{
    public class MomentNotFoundException : Exception
    {
        public MomentNotFoundException(long id)
        : base($"Moment with ID {id} does not exist")
        {
        }
    }
}
