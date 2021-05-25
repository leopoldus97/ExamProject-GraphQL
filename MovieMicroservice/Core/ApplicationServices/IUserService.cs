using System.Collections.Generic;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IUserService {
        User Create(User movie);
        IEnumerable<User> ReadAll();
        User ReadById(int id);
        User Update(int id, User movie);
        User Delete(int id);
    }
}