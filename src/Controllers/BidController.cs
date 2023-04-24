using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public BidController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Bid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBids()
        {
          if (_dbContext.Bids == null)
          {
              return NotFound();
          }
          return await _dbContext.Bids
              .Select(x => BidToDTO(x))
              .ToListAsync();
        }

        // GET: api/Bid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidDTO>> GetBid(long id)
        {
          if (_dbContext.Bids == null)
          {
              return NotFound();
          }
            var bid = await _dbContext.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return BidToDTO(bid);
        }

        // PUT: api/Bid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(long id, BidDTO bidDto)
        {
            if (id != bidDto.Id)
            {
                return BadRequest();
            }

            var bid = await _dbContext.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            bid.Account = bidDto.Account;
            //TODO: to complete

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BidExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Bid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BidDTO>> PostBid(BidDTO bidDto)
        {
          if (_dbContext.Bids == null)
          {
              return Problem("Entity set 'ApiDbContext.Bids'  is null.");
          }
          
          var newBid = new Bid
          {
              Account = bidDto.Account,
              //TODO: to complete
          };
          
            _dbContext.Bids.Add(newBid);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bidDto.Id }, bidDto);
        }

        // DELETE: api/Bid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(long id)
        {
            if (_dbContext.Bids == null)
            {
                return NotFound();
            }
            
            var bid = await _dbContext.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _dbContext.Bids.Remove(bid);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool BidExists(long id)
        {
            return (_dbContext.Bids?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private static BidDTO BidToDTO(Bid bid) =>
            new BidDTO()
            {
                Id = bid.Id,
                Account = bid.Account,
                Type = bid.Type,
                BidQuantity = bid.BidQuantity,
                AskQuantity = bid.AskQuantity,
                BidValue = bid.BidValue,
                Ask = bid.Ask,
                Benchmark = bid.Benchmark,
                BidListDate = bid.BidListDate,
                Commentary = bid.Commentary,
                Security = bid.Security,
                Status = bid.Status,
                Trader = bid.Trader,
                Book = bid.Book,
                CreationName = bid.CreationName,
                CreationDate = bid.CreationDate,
                RevisionName = bid.RevisionName,
                RevisionDate = bid.RevisionDate,
                DealName = bid.DealName,
                DealType = bid.DealType,
                SourceListId = bid.SourceListId,
                Side = bid.Side
            };
    }
}
