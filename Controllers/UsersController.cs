using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.Models;

namespace asp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AspContext _context;

        public UsersController(AspContext context) => _context = context;

        // GET: api/users 
        // Will Return all the Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/users/{id}
        // Will return specific user id=={id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: /api/users
        // Will create a new User
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction("GetUser", new User{ Id=user.Id}, user);
        }

        //PUT: /api/users/{id}
        // Will Edit a specific user user.Id = id
        [HttpPut("{id}")]
        public ActionResult EditUser(int id, User user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/users/{id}
        // Will Delete a specific user user.Id = id
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return CreatedAtAction("GetUser", new User{ Id=user.Id}, user);
        }

    }
}