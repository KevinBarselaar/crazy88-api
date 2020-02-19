using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crazy88Test.Models;

namespace Crazy88Test.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ApiContext _context;

        public SessionController(ApiContext context)
        {
            _context = context;

            if (_context.Session.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TeamItems.
                _context.Session.Add(new Session { ExpiringDateTime = DateTime.Now });
                _context.SaveChanges();
            }
        }

        // GET: api/sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessionItems()
        {
            return await _context.Session
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        // GET: api/sessions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSessionItem(long id)
        {
            var todoItem = await _context.Session.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // GET: api/sessions/timestamp
        [HttpGet("timestamp")]
        public async Task<ActionResult<DateTime>> GetTimestamp()
        {
            var todoItem = _context.Session
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            return todoItem.ExpiringDateTime;
        }

        // POST: api/sessions
        [HttpPost]
        public async Task<ActionResult<Session>> PostSessionItem(Session item)
        {
            //Generate random number for 6 digit login code for unity app
            Random randomCode = new Random();
            item.PlayCode = randomCode.Next(100000, 999999);

            //Set DateTime object without milliseconds
            var timestamp = DateTime.Now;
            item.ExpiringDateTime = new DateTime(timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute, timestamp.Second).AddHours(item.Duration);

            _context.Session.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessionItem), new { id = item.Id }, item);
        }

        // PUT: api/sessions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionItem(long id, Session item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessionItem), new { id = item.Id }, item);
        }

        // DELETE: api/sessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionItem(int id)
        {
            var todoItem = await _context.Session.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Session.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}