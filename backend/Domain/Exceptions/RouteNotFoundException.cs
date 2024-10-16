namespace Domain.Exceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(long id)
        : base($"Маршрут с ID {id} не найден")
        {
        }
    }
}
