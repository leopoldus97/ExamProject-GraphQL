using System.Collections.Generic;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.DomainServices {
    public interface IGenreRepo {
        Genre Create(Genre movie);
        IEnumerable<Genre> ReadAll();
        Genre ReadById(int id);
        Genre Update(int id, Genre movie);
        Genre Delete(int id);
    }
}