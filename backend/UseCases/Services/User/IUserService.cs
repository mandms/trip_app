namespace UseCases.Services.User
{
    public interface IUserService
    {
        public Domain.Entities.User GetById(long id);
    }
}
