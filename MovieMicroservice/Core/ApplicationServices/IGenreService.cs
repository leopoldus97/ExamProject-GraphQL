using System.Collections.Generic;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IGenreService {
        Genre Create(Genre movie);
        IEnumerable<Genre> ReadAll();
        Genre ReadById(int id);
        Genre Update(int id, Genre movie);
        Genre Delete(int id);
    }
}