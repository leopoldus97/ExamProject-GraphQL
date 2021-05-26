using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IGenreService {
        Task<Genre> Create(Genre movie);
        IEnumerable<Genre> ReadAll();
        Genre ReadById(int id);
        Task<Genre> Update(int id, Genre movie);
        Task<Genre> Delete(int id);
    }
}