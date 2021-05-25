using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices {
    public interface IMovieService {
        Task<Movie> Create(Movie movie);
        IEnumerable<Movie> ReadAll();
        Movie ReadById(int id);
        Movie Update(int id, Movie movie);
        Movie Delete(int id);
    }
}