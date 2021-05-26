using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IUserService {
        Task<User> Create(User movie);
        IEnumerable<User> ReadAll();
        User ReadById(int id);
        Task<User> Update(int id, User movie);
        Task<User> Delete(int id);
    }
}