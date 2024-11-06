namespace Domain.Exceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(long id)
        : base($"Route with ID {id} does not exist")
        {
        }
    }
}
