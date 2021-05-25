using System.Collections.Generic;
using System.Linq;
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

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            User user = ReadById(id);
            _context.Users.Remove(user);
            return user ?? null;
        }

        public IEnumerable<User> ReadAll()
        {
            return _context.Users.Include(u => u.RatedMovies).ThenInclude(r => r.RatedMovie).ThenInclude(m => m.Genre).ThenInclude(g => g.Genre);
        }

        public User ReadById(int id)
        {
            return _context.Users.Include(u => u.RatedMovies).ThenInclude(r => r.RatedMovie).ThenInclude(m => m.Genre).ThenInclude(g => g.Genre).FirstOrDefault(u => u.Id == id);
        }

        public User Update(int id, User user)
        {
            User userFromDB = ReadById(id);
            if (CompareUsers(user, userFromDB))
                return null;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return userFromDB;
        }

        private bool CompareUsers(User movie1, User movie2) {
            return movie1.Username == movie2.Username &&
                   movie1.RegistrationDate == movie2.RegistrationDate;
        }
    }
}