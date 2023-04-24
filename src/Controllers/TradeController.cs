using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        /// <inheritdoc />
        public TradeController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Trade
        /// <summary>
        /// Get all Trades.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TradeDTO>>> GetTrades()
        {
            return await _dbContext.Trades
                .Select(x => TradeToDTO(x))
                .ToListAsync();
        }

        // GET: api/Trade/5
        /// <summary>
        /// Get a specific Trade.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TradeDTO>> GetTrade(long id)
        {
            var trade = await _dbContext.Trades.FindAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return TradeToDTO(trade);
        }

        /// <summary>
        /// Update a specific Trade.
        /// </summary>
        /// <param name="id"></param>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrade(long id, TradeDTO tradeDto)
        {
            if (id != tradeDto.Id)
            {
                return BadRequest();
            }

            var trade = await _dbContext.Trades.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            trade.Account = tradeDto.Account;
            //TODO: to complete

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TradeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new Trade.
        /// </summary>
        /// <param name="Trade"></param>
        /// <returns>A newly created Trade</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///     "Id": (auto generated)
        ///     "Account": "SampleAccount",
        ///     "Type": "SampleType",
        ///     "BuyQuantity": 0,
        ///     "SellQuantity": 0,
        ///     "BuyPrice": 0,
        ///     "SellPrice": 0,
        ///     "Benchmark": "SampleBenchmark",
        ///     "TradeDate": "2021-03-01T00:00:00",
        ///     "Security": "SampleSecurity",
        ///     "Status": "SampleStatus",
        ///     "Trader": "SampleTrader",
        ///     "Book": "SampleBook",
        ///     "CreationName": "SampleCreationName",
        ///     "CreationDate": "2021-03-01T00:00:00",
        ///     "RevisionName": "SampleRevisionName",
        ///     "RevisionDate": "2021-03-01T00:00:00",
        ///     "DealName": "SampleDealName",
        ///     "DealType": "SampleDealType",
        ///     "SourceListId": "SampleSourceListId",
        ///     "Side": "SampleSide",
        ///     }
        ///
        /// </remarks>
        // POST: api/Trade
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TradeDTO>> PostTrade(TradeDTO tradeDto)
        {
            if (_dbContext.Trades == null)
            {
                return Problem("Entity set 'ApiDbContext.Trades'  is null.");
            }

            var newRule = new Trade()
            {
                Account = tradeDto.Account.IsNullOrEmpty() ? "SampleAccount" : tradeDto.Account,
                Type = tradeDto.Type.IsNullOrEmpty() ? "SampleType" : tradeDto.Type,
                BuyQuantity = tradeDto.BuyQuantity == 0 ? 0 : tradeDto.BuyQuantity,
                SellQuantity = tradeDto.SellQuantity == 0 ? 0 : tradeDto.SellQuantity,
                BuyPrice = tradeDto.BuyPrice == 0 ? 0 : tradeDto.BuyPrice,
                SellPrice = tradeDto.SellPrice == 0 ? 0 : tradeDto.SellPrice,
                Benchmark = tradeDto.Benchmark.IsNullOrEmpty() ? "SampleBenchmark" : tradeDto.Benchmark,
                TradeDate = tradeDto.TradeDate == DateTime.MinValue ? DateTime.MinValue : tradeDto.TradeDate,
                Security = tradeDto.Security.IsNullOrEmpty() ? "SampleSecurity" : tradeDto.Security,
                Status = tradeDto.Status.IsNullOrEmpty() ? "SampleStatus" : tradeDto.Status,
                Trader = tradeDto.Trader.IsNullOrEmpty() ? "SampleTrader" : tradeDto.Trader,
                Book = tradeDto.Book.IsNullOrEmpty() ? "SampleBook" : tradeDto.Book,
                CreationName = tradeDto.CreationName.IsNullOrEmpty() ? "SampleCreationName" : tradeDto.CreationName,
                CreationDate = tradeDto.CreationDate == DateTime.MinValue ? DateTime.MinValue : tradeDto.CreationDate,
                RevisionName = tradeDto.RevisionName.IsNullOrEmpty() ? "SampleRevisionName" : tradeDto.RevisionName,
                RevisionDate = tradeDto.RevisionDate == DateTime.MinValue ? DateTime.MinValue : tradeDto.RevisionDate,
                DealName = tradeDto.DealName.IsNullOrEmpty() ? "SampleDealName" : tradeDto.DealName,
                DealType = tradeDto.DealType.IsNullOrEmpty() ? "SampleDealType" : tradeDto.DealType,
                SourceListId = tradeDto.SourceListId.IsNullOrEmpty() ? "SampleSourceListId" : tradeDto.SourceListId,
                Side = tradeDto.Side.IsNullOrEmpty() ? "SampleSide" : tradeDto.Side,
            };

            _dbContext.Trades.Add(newRule);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTrade", new { id = tradeDto.Id }, tradeDto);
        }

        // DELETE: api/Trade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(long id)
        {
            if (_dbContext.Trades == null)
            {
                return NotFound();
            }

            var trade = await _dbContext.Trades.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            _dbContext.Trades.Remove(trade);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeExists(long id)
        {
            return (_dbContext.Trades?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static TradeDTO TradeToDTO(Trade trade) =>
            new TradeDTO()
            {
                Id = trade.Id,
                Account = trade.Account,
                Type = trade.Type,
                BuyQuantity = trade.BuyQuantity,
                SellQuantity = trade.SellQuantity,
                BuyPrice = trade.BuyPrice,
                SellPrice = trade.SellPrice,
                Benchmark = trade.Benchmark,
                TradeDate = trade.TradeDate,
                Security = trade.Security,
                Status = trade.Status,
                Trader = trade.Trader,
                Book = trade.Book,
                CreationName = trade.CreationName,
                RevisionName = trade.RevisionName,
                RevisionDate = trade.RevisionDate,
                DealName = trade.DealName,
                DealType = trade.DealType,
                SourceListId = trade.SourceListId,
                Side = trade.Side
            };
    }
}