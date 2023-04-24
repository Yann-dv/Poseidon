using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        /// <summary>
        /// Get all Rules.
        /// </summary>
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
        /// <summary>
        /// Get a specific Rules.
        /// </summary>
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
        /// <summary>
        /// Update a specific Rule.
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(long id, RuleDTO ruleDto)
        {
            if (id != ruleDto.Id)
            {
                return BadRequest();
            }

            var rule = await _dbContext.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            rule.Description = rule.Description != ruleDto.Description ? ruleDto.Description : rule.Description;
            rule.Name = rule.Name != ruleDto.Name ? ruleDto.Name : rule.Name;
            rule.Json = rule.Json != ruleDto.Json ? ruleDto.Json : rule.Json;
            rule.Template = rule.Template != ruleDto.Template ? ruleDto.Template : rule.Template;
            rule.SqlStr = rule.SqlStr != ruleDto.SqlStr ? ruleDto.SqlStr : rule.SqlStr;
            rule.SqlPart = rule.SqlPart != ruleDto.SqlPart ? ruleDto.SqlPart : rule.SqlPart;

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
        ///        "Id": (auto generated),
        ///        "Name": "Rule1",
        ///        "Description": "Rule1 Description",
        ///        "Json": "Json1",
        ///        "Template": "Template1",
        ///        "SqlStr": "SqlStr1",
        ///        "SqlPart": "SqlPart1"
        ///     }
        ///
        /// </remarks>
        // POST: api/Rule
        [HttpPost]
        public async Task<ActionResult<RuleDTO>> PostRule(RuleDTO ruleDto)
        {
            if (_dbContext.Rules == null)
            {
                return Problem("Entity set 'ApiDbContext.Rules'  is null.");
            }

            var newRule = new Rule
            {
                Description = ruleDto.Description.IsNullOrEmpty() ? "Default Description" : ruleDto.Description,
                Name = ruleDto.Name.IsNullOrEmpty() ? "Default Name" : ruleDto.Name,
                Json = ruleDto.Json.IsNullOrEmpty() ? "Default Json" : ruleDto.Json,
                Template = ruleDto.Template.IsNullOrEmpty() ? "Default Template" : ruleDto.Template,
                SqlStr = ruleDto.SqlStr.IsNullOrEmpty() ? "Default SqlStr" : ruleDto.SqlStr,
                SqlPart = ruleDto.SqlPart.IsNullOrEmpty() ? "Default SqlPart" : ruleDto.SqlPart
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