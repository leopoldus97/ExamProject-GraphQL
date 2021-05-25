using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieMicroservice.Core.ApplicationServices;
using MovieMicroservice.Core.Entity;
using Newtonsoft.Json;

namespace MovieMicroservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;
        public GenreController(IGenreService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var genres = _service.ReadAll();
            if (genres.Any())
                return Ok(genres);
            else
                return NoContent();
        }
        [HttpGet("id")]
        public ActionResult GetById(int id)
        {
            var genre = _service.ReadById(id);
            if (genre != null)
                return Ok(genre);
            else
                return NoContent();
        }
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            var createdGenre = _service.Create(genre);
            if (createdGenre != null)
                return Ok(createdGenre);
            else
                return NoContent();
        }
        [HttpPut("id")]
        public ActionResult Put(int id, [FromBody] Genre genre)
        {
            var updatedGenre = _service.Update(id, genre);
            if (updatedGenre != null)
                return Ok(updatedGenre);
            else
                return NoContent();
        }
        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            var deletedGenre = _service.Delete(id);
            if (deletedGenre != null)
                return Ok(deletedGenre);
            else
                return NoContent();
        }
    }
}