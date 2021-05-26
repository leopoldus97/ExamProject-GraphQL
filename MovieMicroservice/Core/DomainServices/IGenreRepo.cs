using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.DomainServices {
    public interface IGenreRepo {
        Task<Genre> Create(Genre genre);
        IEnumerable<Genre> ReadAll();
        Genre ReadById(int id);
        Task<Genre> Update(int id, Genre genre);
        Task<Genre> Delete(int id);
    }
}