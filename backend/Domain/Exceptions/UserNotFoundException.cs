namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long id = 0)
        : base((id == 0) ? "User not found" : $"User with ID {id} does not exist")
        {
        }
    }
}
