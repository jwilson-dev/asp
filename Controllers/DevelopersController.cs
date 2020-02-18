using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.Models;

namespace asp.Controllers
{
    [Route("api/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly AspContext _context;

        public DevelopersController(AspContext context) => _context = context;

        // GET: api/developers 
        // Will Return all the Developers
        [HttpGet]
        public ActionResult<IEnumerable<Developer>> GetDevelopers()
        {
            return _context.Developers;
        }

        // GET: api/developers/{id}
        // Will return specific developer id=={id}
        [HttpGet("{id}")]
        public ActionResult<Developer> Getdeveloper(int id)
        {
            var developer = _context.Developers.Find(id);

            if(developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        // POST: /api/developers
        // Will create a new developer
        [HttpPost]
        public ActionResult<Developer> Createdeveloper(Developer developer)
        {
            _context.Developers.Add(developer);
            _context.SaveChanges();

            return CreatedAtAction("GetDeveloper", new Developer{ Id=developer.Id}, developer);
        }

        //PUT: /api/developers/{id}
        // Will Edit a specific developer developer.Id = id
        [HttpPut("{id}")]
        public ActionResult Editdeveloper(int id, Developer developer)
        {
            if(id != developer.Id)
            {
                return BadRequest();
            }

            _context.Entry(developer).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/developers/{id}
        // Will Delete a specific developer developer.Id = id
        [HttpDelete("{id}")]
        public ActionResult<Developer> Deletedeveloper(int id)
        {
            var developer = _context.Developers.Find(id);

            if(developer == null)
            {
                return NotFound();
            }

            _context.Developers.Remove(developer);
            _context.SaveChanges();

            return CreatedAtAction("GetDeveloper", new Developer{ Id=developer.Id}, developer);
        }

    }
}