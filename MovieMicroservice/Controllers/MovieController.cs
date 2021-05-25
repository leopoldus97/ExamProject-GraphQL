using Microsoft.AspNetCore.Mvc;
using MovieMicroservice.Core.ApplicationServices;
using MovieMicroservice.Core.Entity;
using System.Linq;

namespace MovieMicroservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;
        public MovieController(IMovieService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var movies = _service.ReadAll();
            if (movies.Any())
                return Ok(movies);
            else
                return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var movie = _service.ReadById(id);
            if (movie != null)
                return Ok(movie);
            else
                return NoContent();
        }
        [HttpPost]
        public ActionResult Post([FromBody] Movie movie)
        {
            var createdMovie = _service.Create(movie);
            if (createdMovie != null)
                return Ok(createdMovie);
            else
                return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Movie movie)
        {
            var updatedMovie = _service.Update(id, movie);
            if (updatedMovie != null)
                return Ok(updatedMovie);
            else
                return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deletedMovie = _service.Delete(id);
            if (deletedMovie != null)
                return Ok(deletedMovie);
            else
                return NoContent();
        }
    }
}
