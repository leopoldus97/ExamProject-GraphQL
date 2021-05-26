using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Genre> Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> Delete(int id)
        {
            Genre genre = ReadById(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return genre ?? null;
        }

        public IEnumerable<Genre> ReadAll()
        {
            return _context.Genres.Include(g => g.Movies).ThenInclude(m => m.Movie); 
        }

        public Genre ReadById(int id)
        {
            return _context.Genres.AsNoTracking().Include(g => g.Movies).ThenInclude(m => m.Movie).FirstOrDefault(g => g.Id == id);
        }

        public async Task<Genre> Update(int id, Genre genre)
        {
            Genre genreFromDB = ReadById(id);
            if (CompareGenres(genre, genreFromDB))
                return null;
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return genreFromDB;
        }

        private bool CompareGenres(Genre genre1, Genre genre2) {
            return genre1.Name == genre2.Name;
        }
    }
}