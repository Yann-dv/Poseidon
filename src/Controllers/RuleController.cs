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
    public class RuleController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public RuleController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Rule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleDTO>>> GetRule()
        {
          if (_dbContext.Rules == null)
          {
              return NotFound();
          }
          return await _dbContext.Rules
              .Select(x => RuleToDTO(x))
              .ToListAsync();
        }

        // GET: api/Rule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleDTO>> GetRule(long id)
        {
            var rule = await _dbContext.Rules.FindAsync(id);

            if (rule == null)
            {
                return NotFound();
            }

            return RuleToDTO(rule);
        }

        // PUT: api/Rule/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(long id, RuleDTO ruleDto)
        {
            if (id != ruleDto.Id)
            {
                return BadRequest();
            }
            
            var ruleItem = await _dbContext.Rules.FindAsync(id);
            if (ruleItem == null)
            {
                return NotFound();
            }

            ruleItem.Description = ruleDto.Description;
            //TODO: to complete

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RuleExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new Rule.
        /// </summary>
        /// <param name="Rule"></param>
        /// <returns>A newly created Rule</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Id": (auto generated)
        ///     }
        ///
        /// </remarks>
        // POST: api/Rule
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleDTO>> PostRule(RuleDTO ruleDto)
        {
          if (_dbContext.Rules == null)
          {
              return Problem("Entity set 'ApiDbContext.Rules'  is null.");
          }
          var newRule = new Rule
          {
              Description = ruleDto.Description,
              //TODO: to complete
          };
          
            _dbContext.Rules.Add(newRule);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRule), new { id = ruleDto.Id }, ruleDto);
        }

        /// <summary>
        /// Deletes a specific Rule.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // DELETE: api/Rule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(long id)
        {
            var rule = await _dbContext.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            _dbContext.Rules.Remove(rule);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleExists(long id)
        {
            return (_dbContext.Rules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private static RuleDTO RuleToDTO(Rule rule) =>
            new RuleDTO()
            {
                Id = rule.Id,
                Name = rule.Name,
                Description = rule.Description,
                Json = rule.Json,
                Template = rule.Template,
                SqlStr = rule.SqlStr,
                SqlPart = rule.SqlPart
            };
    }
}
