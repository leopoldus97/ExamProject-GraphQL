using System.Collections.Generic;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.DomainServices {
    public interface IUserRepo {
        User Create(User movie);
        IEnumerable<User> ReadAll();
        User ReadById(int id);
        User Update(int id, User movie);
        User Delete(int id);
    }
}