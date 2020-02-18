using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.Models;

namespace asp.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly AspContext _context;

        public PlayersController(AspContext context) => _context = context;

        // GET: api/players 
        // Will Return all the Players
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return _context.Players;
        }

        // GET: api/players/{id}
        // Will return specific player id=={id}
        [HttpGet("{id}")]
        public ActionResult<Player> Getplayer(int id)
        {
            var player = _context.Players.Find(id);

            if(player == null)
            {
                return NotFound();
            }

            return player;
        }

        // POST: /api/players
        // Will create a new player
        [HttpPost]
        public ActionResult<Player> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();

            return CreatedAtAction("GetPlayer", new Player{ Id=player.Id}, player);
        }

        //PUT: /api/players/{id}
        // Will Edit a specific player player.Id = id
        [HttpPut("{id}")]
        public ActionResult EditPlayer(int id, Player player)
        {
            if(id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/players/{id}
        // Will Delete a specific player player.Id = id
        [HttpDelete("{id}")]
        public ActionResult<Player> DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);

            if(player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            _context.SaveChanges();

            return CreatedAtAction("GetPlayer", new Player{ Id=player.Id}, player);
        }

    }
}