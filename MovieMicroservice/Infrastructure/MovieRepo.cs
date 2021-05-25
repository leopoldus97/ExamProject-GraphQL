using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Infrastructure
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieContext _context;
        public MovieRepo(MovieContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public Movie Delete(int id)
        {
            Movie movie = ReadById(id);
            _context.Movies.Remove(movie);
            return movie ?? null;
        }

        public IEnumerable<Movie> ReadAll()
        {
            return _context.Movies.Include(m => m.Genre).ThenInclude(g => g.Genre).Include(m => m.Ratings).ThenInclude(r => r.RatedBy);
        }

        public Movie ReadById(int id)
        {
            return _context.Movies.Include(m => m.Genre).ThenInclude(g => g.Genre).Include(m => m.Ratings).ThenInclude(r => r.RatedBy).FirstOrDefault(m => m.Id == id);
        }

        public Movie Update(int id, Movie movie)
        {
            Movie movieFromDB = ReadById(id);
            if (CompareMovies(movie, movieFromDB))
                return null;
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
            return movieFromDB;
        }

        private bool CompareMovies(Movie movie1, Movie movie2) {
            return movie1.ReleaseDate == movie2.ReleaseDate &&
                   movie1.Title == movie2.Title && 
                   movie1.Duration == movie2.Duration;
        }
    }
}