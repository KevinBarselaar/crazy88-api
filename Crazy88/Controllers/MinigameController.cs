using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crazy88Test.Models;

namespace Crazy88Test.Controllers
{
    [Route("api/minigames")]
    [ApiController]
    public class MinigameController : ControllerBase
    {
        private readonly ApiContext _context;

        public MinigameController(ApiContext context)
        {
            _context = context;

            if (_context.Minigame.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TeamItems.
                _context.Minigame.Add(new Minigame { Name = "First SessionObj", Description = "Placeholder description", MaxScore = 100, MinScore = 0, CardImage = "Placeholder card image", QRValue = "Placeholder QR value", Location = "Placeholder location", Active = true});
                _context.SaveChanges();
            }
        }

        // GET: api/minigames
        [HttpGet("object")]
        public async Task<ActionResult<Minigames>> GetMinigameItems()
        {
            Minigames minigames = new Minigames();
            minigames.minigames = await _context.Minigame.ToArrayAsync();
            return minigames;
        }

        // GET: api/teams/view
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Minigame>>> GetLeaderboardItemsForView()
        {
            return await _context.Minigame.ToListAsync();
        }

        // GET: api/minigames/{id]
        [HttpGet("{id}")]
        public async Task<ActionResult<Minigame>> GetMinigameItem(long id)
        {
            var todoItem = await _context.Minigame.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/minigames
        [HttpPost]
        public async Task<ActionResult<Minigame>> PostMinigameItem(Minigame item)
        {
            _context.Minigame.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMinigameItem), new { id = item.Id }, item);
        }

        // PUT: api/minigames/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMinigameItem(long id, Minigame item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/minigames/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMinigameItem(int id)
        {
            var todoItem = await _context.Minigame.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Minigame.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}