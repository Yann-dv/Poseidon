using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PoseidonApi.Data;
using PoseidonApi.DTO;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        /// <inheritdoc />
        public RatingController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Rating
        /// <summary>
        /// Get all Ratings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRating()
        {
            return await _dbContext.Ratings
                .Select(x => RatingToDTO(x))
                .ToListAsync();
        }

        // GET: api/Rating/5
        /// <summary>
        /// Get a specific Rating.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Update a specific Rating.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratingDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(long id, RatingDTO ratingDto, ApiDbContext? dbContext)
        {
            dbContext ??= _dbContext;
            
            if (id != ratingDto.Id)
            {
                return BadRequest();
            }

            var rating = await dbContext.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            rating.MoodysRating = rating.MoodysRating != ratingDto.MoodysRating ? ratingDto.MoodysRating : rating.MoodysRating;
            rating.FitchRating = rating.FitchRating != ratingDto.FitchRating ? ratingDto.FitchRating : rating.FitchRating;
            rating.SandPRating = rating.SandPRating != ratingDto.SandPRating ? ratingDto.SandPRating : rating.SandPRating;
            rating.OrderNumber = rating.OrderNumber != ratingDto.OrderNumber ? ratingDto.OrderNumber : rating.OrderNumber;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RatingExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a new Rating.
        /// </summary>
        /// <param name="ratingDto"></param>
        /// <remarks>
        /// <returns></returns>
        /// /// Request sample:
        ///
        ///     POST
        ///     {
        ///         "Id": (auto generated),
        ///         "MoodysRating": "A1",
        ///         "FitchRating": "A+",
        ///         "SandPRating": "A+",
        ///         "OrderNumber": 1
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<RatingDTO>> PostRating(RatingDTO ratingDto)
        {
            if (_dbContext.Ratings == null)
            {
                return Problem("Entity set 'ApiDbContext.Rating'  is null.");
            }

            var newRating = new Rating
            {
                MoodysRating = ratingDto.MoodysRating.IsNullOrEmpty() ? "A1" : ratingDto.MoodysRating,
                FitchRating = ratingDto.FitchRating.IsNullOrEmpty() ? "A+" : ratingDto.FitchRating,
                SandPRating = ratingDto.SandPRating.IsNullOrEmpty() ? "A+" : ratingDto.SandPRating,
                OrderNumber = ratingDto.OrderNumber == 0 ? 0 : ratingDto.OrderNumber
            };
          
            _dbContext.Ratings.Add(newRating);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRating), new { id = ratingDto.Id }, ratingDto);
        }

        // DELETE: api/Rating/5
        /// <summary>
        /// Delete a specific Rating.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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