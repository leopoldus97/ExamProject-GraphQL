using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMicroservice.Core.DomainServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Core.ApplicationServices.Implementations
{
    public class MovieService : IMovieService {
        private readonly IMovieRepo _repo;
        public MovieService(IMovieRepo repo)
        {
            _repo = repo;
        }

        public async Task<Movie> Create(Movie movie)
        {
            if (movie.Id.HasValue)
                throw new Exception("Id should be empty!");
            if (string.IsNullOrEmpty(movie.Title))
                throw new Exception("Title cannot be empty!");
            if (movie.Genre == null)
                throw new Exception("A Genre has to be selected!");
            return await _repo.Create(movie);
        }

        public Movie Delete(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return _repo.Delete(id);
        }

        public IEnumerable<Movie> ReadAll()
        {
            return _repo.ReadAll();
        }

        public Movie ReadById(int id)
        {
            if (id < 1)
                throw new Exception("Id has to be a number bigger than 0!");
            return _repo.ReadById(id);
        }

        public Movie Update(int id, Movie movie)
        {
            if (id < 1 || !movie.Id.HasValue)
                throw new Exception("Id error!");
            if (movie.Id.Value != id)
                throw new Exception("Id mismatch!");
            if (string.IsNullOrEmpty(movie.Title))
                throw new Exception("Title cannot be empty!");
            if (movie.Genre == null)
                throw new Exception("A Genre has to be selected!");
            return _repo.Update(id, movie);
        }
    }
}