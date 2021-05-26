using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.DomainServices {
    public interface IUserRepo {
        Task<User> Create(User user);
        IEnumerable<User> ReadAll();
        User ReadById(int id);
        Task<User> Update(int id, User user);
        Task<User> Delete(int id);
    }
}