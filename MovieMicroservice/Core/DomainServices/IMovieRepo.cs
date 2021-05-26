using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.DomainServices {
    public interface IMovieRepo {
        Task<Movie> Create(Movie movie);
        IEnumerable<Movie> ReadAll();
        Movie ReadById(int id);
        Task<Movie> Update(int id, Movie movie);
        Task<Movie> Delete(int id);
    }
}