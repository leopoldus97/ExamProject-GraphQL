using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieMicroservice.Core.ApplicationServices;
using MovieMicroservice.Core.Entity;

namespace MovieMicroservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var users = _service.ReadAll();
            if (users.Any())
                return Ok(users);
            else
                return NoContent();
        }
        [HttpGet("id")]
        public ActionResult GetById(int id)
        {
            var user = _service.ReadById(id);
            if (user != null)
                return Ok(user);
            else
                return NoContent();
        }
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            var createdUser = _service.Create(user);
            if (createdUser != null)
                return Ok(user);
            else
                return NoContent();
        }
        [HttpPut("id")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            var updatedUser = _service.Update(id, user);
            if (updatedUser != null)
                return Ok(updatedUser);
            else
                return NoContent();
        }
        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            var deletedUser = _service.Delete(id);
            if (deletedUser != null)
                return Ok(deletedUser);
            else
                return NoContent();
        }
    }
}