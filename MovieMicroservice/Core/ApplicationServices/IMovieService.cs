using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IMovieService {
        Task<Movie> Create(Movie movie);
        IEnumerable<Movie> ReadAll();
        Movie ReadById(int id);
        Task<Movie> Update(int id, Movie movie);
        Task<Movie> Delete(int id);
    }
}