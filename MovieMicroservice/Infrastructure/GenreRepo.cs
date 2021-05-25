using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Infrastructure
{
    public class GenreRepo : IGenreRepo
    {
        private readonly MovieContext _context;
        public GenreRepo(MovieContext context)
        {
            _context = context;
        }

        public Genre Create(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(int id)
        {
            Genre genre = ReadById(id);
            _context.Genres.Remove(genre);
            return genre ?? null;
        }

        public IEnumerable<Genre> ReadAll()
        {
            return _context.Genres.Include(g => g.Movies).ThenInclude(m => m.Movie); 
        }

        public Genre ReadById(int id)
        {
            return _context.Genres.Include(g => g.Movies).ThenInclude(m => m.Movie).FirstOrDefault(g => g.Id == id);
        }

        public Genre Update(int id, Genre genre)
        {
            Genre genreFromDB = ReadById(id);
            if (CompareGenres(genre, genreFromDB))
                return null;
            _context.Entry(genre).State = EntityState.Modified;
            _context.SaveChanges();
            return genreFromDB;
        }

        private bool CompareGenres(Genre movie1, Genre movie2) {
            return movie1.Name == movie2.Name;
        }
    }
}