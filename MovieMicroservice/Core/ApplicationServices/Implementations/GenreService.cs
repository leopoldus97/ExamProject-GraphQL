using System;
using System.Collections.Generic;
using System.Linq;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepo _repo;
        public GenreService(IGenreRepo repo)
        {
            _repo = repo;
        }
        public Genre Create(Genre genre)
        {
            if (genre.Id.HasValue)
                throw new Exception("Id should be empty!");
            if (string.IsNullOrEmpty(genre.Name))
                throw new Exception("Genre name cannot be empty!");
            return _repo.Create(genre);
        }

        public Genre Delete(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return _repo.Delete(id);
        }

        public IEnumerable<Genre> ReadAll()
        {
            return _repo.ReadAll();
        }

        public Genre ReadById(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return _repo.ReadById(id);
        }

        public Genre Update(int id, Genre genre)
        {
            if (genre.Id.HasValue)
                throw new Exception("Id should be empty!");
            if (string.IsNullOrEmpty(genre.Name))
                throw new Exception("Genre name cannot be empty!");
            return _repo.Create(genre);
        }
    }
}