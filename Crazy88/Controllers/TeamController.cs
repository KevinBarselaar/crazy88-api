using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crazy88Test.Models;

namespace Crazy88Test.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamController(ApiContext context)
        {
            _context = context;

            if (_context.Team.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TeamItems.
                _context.Team.Add(new Team { TeamName = "First Team", Score = 1, Photo = "PLACEHOLDER", Session = 1});
                _context.SaveChanges();
            }
        }

        // GET: api/teams
        [HttpGet("object")]
        public async Task<ActionResult<Teams>> GetLeaderboardItems()
        {
            Teams teams = new Teams();
            teams.teams = await _context.Team.ToArrayAsync();
            return teams;
        }

        // GET: api/teams/view
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetLeaderboardItemsForView()
        {
            return await _context.Team.OrderByDescending(score => score.Score).ToListAsync();
        }

        // GET: api/teams/view
        [HttpGet("session/{id}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsBySession(long id)
        {
            List<Team> session = new List<Team>()
                ;
            foreach (var team in _context.Team)
            {
                if (team.Session == id)
                {
                    session.Add(team);
                }
            }

            return session;
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetLeaderboardItem(long id)
        {
            var todoItem = await _context.Team.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/teams
        [HttpPost]
        public async Task<ActionResult<Team>> PostLeaderboardItem(Team item)
        {
            if (item.Id == -1)
            {
                item.Id = 0;
            }
            
            item.Session = _context.Session.OrderByDescending(p => p.Id).FirstOrDefault().Id;

            _context.Team.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLeaderboardItem), new { id = item.Id }, item);
        }

        // PUT: api/teams/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaderboardItem(long id, Team item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeaderboardItem), new { id = item.Id }, item);
        }

        // DELETE: api/teams/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaderboardItem(int id)
        {
            var todoItem = await _context.Team.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Team.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}