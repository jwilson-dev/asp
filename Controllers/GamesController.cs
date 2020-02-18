using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.Models;

namespace asp.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AspContext _context;

        public GamesController(AspContext context) => _context = context;

        // GET: api/games 
        // Will Return all the Games
        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetGames()
        {
            return _context.Games;
        }

        // GET: api/games/{id}
        // Will return specific game id=={id}
        [HttpGet("{id}")]
        public ActionResult<Game> GetGame(int id)
        {
            var game = _context.Games.Find(id);

            if(game == null)
            {
                return NotFound();
            }

            return game;
        }

        // POST: /api/games
        // Will create a new game
        [HttpPost]
        public ActionResult<Game> CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtAction("GetGame", new Game{ Id=game.Id}, game);
        }

        //PUT: /api/games/{id}
        // Will Edit a specific game game.Id = id
        [HttpPut("{id}")]
        public ActionResult EditGame(int id, Game game)
        {
            if(id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/games/{id}
        // Will Delete a specific game game.Id = id
        [HttpDelete("{id}")]
        public ActionResult<Game> DeleteGame(int id)
        {
            var game = _context.Games.Find(id);

            if(game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

            return CreatedAtAction("GetGame", new Game{ Id=game.Id}, game);
        }

    }
}