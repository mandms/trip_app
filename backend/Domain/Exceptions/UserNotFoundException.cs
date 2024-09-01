namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long id) 
        :base($"Пользователь с ID {id} не найден")
        { 
        }
    }
}
