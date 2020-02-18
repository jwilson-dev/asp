using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.Models;

namespace asp.Controllers
{
    [Route("api/enres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AspContext _context;

        public GenresController(AspContext context) => _context = context;

        // GET: api/genres 
        // Will Return all the Genres
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetGenres()
        {
            return _context.Genres;
        }

        // GET: api/genres/{id}
        // Will return specific genre id=={id}
        [HttpGet("{id}")]
        public ActionResult<Genre> GetGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if(genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // POST: /api/genres
        // Will create a new genre
        [HttpPost]
        public ActionResult<Genre> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();

            return CreatedAtAction("GetGenre", new Genre{ Id=genre.Id}, genre);
        }

        //PUT: /api/genres/{id}
        // Will Edit a specific genre genre.Id = id
        [HttpPut("{id}")]
        public ActionResult EditGenre(int id, Genre genre)
        {
            if(id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/genres/{id}
        // Will Delete a specific genre genre.Id = id
        [HttpDelete("{id}")]
        public ActionResult<Genre> DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if(genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return CreatedAtAction("GetGenre", new Genre{ Id=genre.Id}, genre);
        }

    }
}