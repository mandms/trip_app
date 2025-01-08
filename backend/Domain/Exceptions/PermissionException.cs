namespace Domain.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException()
            : base("You do not have permission") { }
    }
}
