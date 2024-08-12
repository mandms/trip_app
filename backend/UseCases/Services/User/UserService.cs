using Infrastructure.Foundation.Repositories;

namespace WebApi.Services.User
{
    public class UserService: IUserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository) 
        {
            _repository = repository;
        }

        public Domain.Entities.User Get()
        {
            return _repository.GetById(id: 1);
        }
    }
}
