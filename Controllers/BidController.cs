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
    public class BidController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        /// <inheritdoc />
        public BidController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Bid
        /// <summary>
        /// Get all Bids.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get a specific Bid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Update a specific Bid.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bidDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(long id, BidDTO bidDto, ApiDbContext? dbContext)
        {
            dbContext ??= _dbContext;
            
            if (id != bidDto.Id)
            {
                return BadRequest();
            }

            var bid = await dbContext.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            const double tolerance = 0.000000001;

            bid.Account = bid.Account != bidDto.Account ? bidDto.Account : bid.Account;
            bid.Type = bid.Type != bidDto.Type ? bidDto.Type : bid.Type;
            bid.BidQuantity = Math.Abs(bid.BidQuantity - bidDto.BidQuantity) > tolerance ? bidDto.BidQuantity : bid.BidQuantity;
            bid.AskQuantity = Math.Abs(bid.AskQuantity - bidDto.AskQuantity) > tolerance ? bidDto.AskQuantity : bid.AskQuantity;
            bid.BidValue = Math.Abs(bid.BidValue - bidDto.BidValue) > tolerance ? bidDto.BidValue : bid.BidValue;
            bid.Ask = Math.Abs(bid.Ask - bidDto.Ask) > tolerance ? bidDto.Ask : bid.Ask;
            bid.Benchmark = bid.Benchmark != bidDto.Benchmark ? bidDto.Benchmark : bid.Benchmark;
            bid.BidListDate = bid.BidListDate != bidDto.BidListDate ? bidDto.BidListDate : bid.BidListDate;
            bid.Commentary = bid.Commentary != bidDto.Commentary ? bidDto.Commentary : bid.Commentary;
            bid.Security = bid.Security != bidDto.Security ? bidDto.Security : bid.Security;
            bid.Status = bid.Status != bidDto.Status ? bidDto.Status : bid.Status;
            bid.Trader = bid.Trader != bidDto.Trader ? bidDto.Trader : bid.Trader;
            bid.Book = bid.Book != bidDto.Book ? bidDto.Book : bid.Book;
            bid.CreationName = bid.CreationName != bidDto.CreationName ? bidDto.CreationName : bid.CreationName;
            bid.CreationDate = bid.CreationDate != bidDto.CreationDate ? bidDto.CreationDate : bid.CreationDate;
            bid.RevisionName = bid.RevisionName != bidDto.RevisionName ? bidDto.RevisionName : bid.RevisionName;
            bid.RevisionDate = bid.RevisionDate != bidDto.RevisionDate ? bidDto.RevisionDate : bid.RevisionDate;
            bid.DealName = bid.DealName != bidDto.DealName ? bidDto.DealName : bid.DealName;
            bid.DealType = bid.DealType != bidDto.DealType ? bidDto.DealType : bid.DealType;
            bid.SourceListId = bid.SourceListId != bidDto.SourceListId ? bidDto.SourceListId : bid.SourceListId;
            bid.Side = bid.Side != bidDto.Side ? bidDto.Side : bid.Side;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BidExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Bid
        /// <summary>
        /// Create a new Bid.
        /// </summary>
        /// <param name="bidDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///     "Id": (auto generated),
        ///     "Account": "Account",
        ///     "Type": "Type",
        ///     "BidQuantity": 0,
        ///     "AskQuantity": 0,
        ///     "BidValue": 0,
        ///     "Ask": 0,
        ///     "Benchmark": "Benchmark",
        ///     "BidListDate": "2021-03-01T00:00:00",
        ///     "Commentary": "Commentary",
        ///     "Security": "Security",
        ///     "Status": "Status",
        ///     "Trader": "Trader",
        ///     "Book": "Book",
        ///     "CreationName": "CreationName",
        ///     "CreationDate": "2021-03-01T00:00:00",
        ///     "RevisionName": "RevisionName",
        ///     "RevisionDate": "2021-03-01T00:00:00",
        ///     "DealName": "DealName",
        ///     "DealType": "DealType",
        ///     "SourceListId": "SourceListId",
        ///     "Side": "Side"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<BidDTO>> PostBid(BidDTO bidDto)
        {
          if (_dbContext.Bids == null)
          {
              return Problem("Entity set 'ApiDbContext.Bids'  is null.");
          }
          
          var newBid = new Bid
          {
              Account = bidDto.Account.IsNullOrEmpty() ? "DefaultAccoun" : bidDto.Account,
              Type = bidDto.Type.IsNullOrEmpty() ? "DefaultType" : bidDto.Type,
                BidQuantity = bidDto.BidQuantity == 0 ? 0 : bidDto.BidQuantity,
                AskQuantity = bidDto.AskQuantity == 0 ? 0 : bidDto.AskQuantity,
                BidValue = bidDto.BidValue == 0 ? 0 : bidDto.BidValue,
                Ask = bidDto.Ask == 0 ? 0 : bidDto.Ask,
                Benchmark = bidDto.Benchmark.IsNullOrEmpty() ? "DefaultBenchmark" : bidDto.Benchmark,
                BidListDate = bidDto.BidListDate == DateTime.MinValue ? DateTime.Now : bidDto.BidListDate,
                Commentary = bidDto.Commentary.IsNullOrEmpty() ? "DefaultCommentary" : bidDto.Commentary,
                Security = bidDto.Security.IsNullOrEmpty() ? "DefaultSecurity" : bidDto.Security,
                Status = bidDto.Status.IsNullOrEmpty() ? "DefaultStatus" : bidDto.Status,
                Trader = bidDto.Trader.IsNullOrEmpty() ? "DefaultTrader" : bidDto.Trader,
                Book = bidDto.Book.IsNullOrEmpty() ? "DefaultBook" : bidDto.Book,
                CreationName = bidDto.CreationName.IsNullOrEmpty() ? "DefaultCreationName" : bidDto.CreationName,
                CreationDate = bidDto.CreationDate == DateTime.MinValue ? DateTime.Now : bidDto.CreationDate,
                RevisionName = bidDto.RevisionName.IsNullOrEmpty() ? "DefaultRevisionName" : bidDto.RevisionName,
                RevisionDate = bidDto.RevisionDate == DateTime.MinValue ? DateTime.Now : bidDto.RevisionDate,
                DealName = bidDto.DealName.IsNullOrEmpty() ? "DefaultDealName" : bidDto.DealName,
                DealType = bidDto.DealType.IsNullOrEmpty() ? "DefaultDealType" : bidDto.DealType,
                SourceListId = bidDto.SourceListId.IsNullOrEmpty() ? "DefaultSourceListId" : bidDto.SourceListId,
                Side = bidDto.Side.IsNullOrEmpty() ? "DefaultSide" : bidDto.Side
          };
          
            _dbContext.Bids.Add(newBid);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bidDto.Id }, bidDto);
        }

        // DELETE: api/Bid/5
        /// <summary>
        /// Delete a specific Bid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
