using Domain.Repositories;

namespace UseCases.Services.User
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public Domain.Entities.User GetById(long id)
        {
            return _repository.GetById(id);
        }
    }
}
