namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long id) 
        :base($"User with ID {id} does not exist")
        { 
        }
    }
}
