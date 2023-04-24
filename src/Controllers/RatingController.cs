using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public RatingController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRating()
        {
            return await _dbContext.Ratings
                .Select(x => RatingToDTO(x))
                .ToListAsync();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(long id)
        {
            var rating = await _dbContext.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return RatingToDTO(rating);
        }

        // PUT: api/Rating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(long id, RatingDTO ratingDto)
        {
            if (id != ratingDto.Id)
            {
                return BadRequest();
            }

            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            rating.MoodysRating = ratingDto.MoodysRating;
            //TODO: to complete

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RatingExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(RatingDTO ratingDto)
        {
            if (_dbContext.Ratings == null)
            {
                return Problem("Entity set 'ApiDbContext.Rating'  is null.");
            }

            var newRating = new Rating
            {
                MoodysRating = ratingDto.MoodysRating
                //TODO: to complete
            };
          
            _dbContext.Ratings.Add(newRating);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRating), new { id = ratingDto.Id }, ratingDto);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(long id)
        {
            if (_dbContext.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _dbContext.Ratings.Remove(rating);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(long id)
        {
            return (_dbContext.Ratings?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static RatingDTO RatingToDTO(Rating rating) =>
            new RatingDTO()
            {
                Id = rating.Id,
                MoodysRating = rating.MoodysRating,
                SandPRating = rating.SandPRating,
                FitchRating = rating.FitchRating,
                OrderNumber = rating.OrderNumber
            };
    }
}