using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }
        public async Task<User> Create(User user)
        {
            if (user.Id.HasValue)
                throw new Exception("Id should be empty!");
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username cannot be empty!");
            return await _repo.Create(user);
        }

        public async Task<User> Delete(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return await _repo.Delete(id);
        }

        public IEnumerable<User> ReadAll()
        {
            return _repo.ReadAll();
        }

        public User ReadById(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return _repo.ReadById(id);
        }

        public async Task<User> Update(int id, User user)
        {
            if (user.Id.HasValue)
                throw new Exception("Id should be empty!");
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username cannot be empty!");
            return await _repo.Create(user);
        }
    }
}