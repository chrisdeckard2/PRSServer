using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSServer.Data;
using PRSServer.Models;

namespace PRSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSServerContext _context;

        public RequestsController(PRSServerContext context)
        {
            _context = context;
        }

        [HttpGet("reviews/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int id) {
            return await (from r in _context.Request
                          where r.Id != id
                          select r).ToListAsync();
        }
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByStatus(string status) {
            return await _context.Request
                                    .Include(x => x.Users)
                                    .Where(x => x.Status == status).ToListAsync();

        }

            // GET: api/Requests
            [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            //var request = await _context.Request.FindAsync(id);

            var request = await (from r in _context.Request
                                 where r.Id == id
                                 select r).Include(x => x.Users).SingleOrDefaultAsync();

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }




        [HttpPut("review/{id}")]          //Review Status 
        public async Task<ActionResult<Request>> Review(int id) {
            var request = await _context.Request.FindAsync(id);

            if (request == null) {
                return NotFound();
            } else if (request.Total <= 50) {
                request.Status = "Approved";
            } else {
                request.Status = "Review";
            }
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();
            return request;

        }




        [HttpPut("approve/{id}")]        //Approve Status 
        public async Task<ActionResult<Request>> Approve(int id) {
            var request = await _context.Request.FindAsync(id);

            if (request == null) {
                return NotFound();
            }
            request.Status = "Approved";
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();
            return request;
        }





        [HttpPut("reject/{id}")]        //Reject Status 
        public async Task<ActionResult<Request>> Reject(int id) {
            var request = await _context.Request.FindAsync(id);

            if (request == null) {
                return NotFound();
            }
            request.Status = "Rejected";
            _context.Entry(request).State = EntityState.Modified;
            
            _context.SaveChanges();
            return request;
        }




        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {

            request.DeliveryMode = "Pickup";
            request.Status = "New";
            request.Total = 0;
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }



        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
