using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Infrastructure
{
    public class UserRepo : IUserRepo
    {
        private readonly MovieContext _context;
        public UserRepo(MovieContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            User user = ReadById(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user ?? null;
        }

        public IEnumerable<User> ReadAll()
        {
            return _context.Users.Include(u => u.RatedMovies).ThenInclude(r => r.RatedMovie).ThenInclude(m => m.Genre).ThenInclude(g => g.Genre);
        }

        public User ReadById(int id)
        {
            return _context.Users.AsNoTracking().Include(u => u.RatedMovies).ThenInclude(r => r.RatedMovie).ThenInclude(m => m.Genre).ThenInclude(g => g.Genre).FirstOrDefault(u => u.Id == id);
        }

        public async Task<User> Update(int id, User user)
        {
            User userFromDB = ReadById(id);
            if (CompareUsers(user, userFromDB))
                return null;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userFromDB;
        }

        private bool CompareUsers(User user1, User user2) {
            return user1.Username == user2.Username &&
                   user1.RegistrationDate == user2.RegistrationDate;
        }
    }
}