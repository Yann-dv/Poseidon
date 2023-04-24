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
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public TradeController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Trade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TradeDTO>>> GetTrades()
        {
            if (_dbContext.Trades == null)
            {
                return NotFound();
            }

            return await _dbContext.Trades
                .Select(x => TradeToDTO(x))
                .ToListAsync();
        }

        // GET: api/Trade/5
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

        // PUT: api/Trade/5
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
        ///        "Id": (auto generated)
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
                Account = tradeDto.Account,
                //TODO: to complete
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