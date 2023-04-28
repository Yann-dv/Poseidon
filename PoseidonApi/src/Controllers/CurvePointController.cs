using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class CurvePointController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        /// <inheritdoc />
        public CurvePointController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/CurvePoint
        /// <summary>
        /// Get all CurvePoints.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurvePointDTO>>> GetCurvePoints()
        {
            if (_dbContext.CurvePoints == null)
            {
                return NotFound();
            }

            return await _dbContext.CurvePoints
                .Select(x => CurvePointToDTO(x))
                .ToListAsync();
        }

        // GET: api/CurvePoint/5
        /// <summary>
        /// Get a specific CurvePoint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePointDTO>> GetCurvePoint(long id)
        {
            var curvePoint = await _dbContext.CurvePoints.FindAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return CurvePointToDTO(curvePoint);
        }

        // PUT: api/CurvePoint/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a specific CurvePoint.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curvePointDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurvePoint(long id, CurvePointDTO curvePointDto)
        {
            if (id != curvePointDto.Id)
            {
                return BadRequest();
            }

            var curvePoint = await _dbContext.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            curvePoint.Term = curvePoint.Term != curvePointDto.Term ? curvePointDto.Term : curvePoint.Term;
            curvePoint.Value = curvePoint.Value != curvePointDto.Value ? curvePointDto.Value : curvePoint.Value;
            curvePoint.CurveId = curvePoint.CurveId != curvePointDto.CurveId ? curvePointDto.CurveId : curvePoint.CurveId;
            curvePoint.AsOfDate = curvePoint.AsOfDate != curvePointDto.AsOfDate ? curvePointDto.AsOfDate : curvePoint.AsOfDate;
            curvePoint.CreationDate = curvePoint.CreationDate != curvePointDto.CreationDate ? curvePointDto.CreationDate : curvePoint.CreationDate;
            

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CurvePointExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/CurvePoint
        /// <summary>
        /// Create a new CurvePoint.
        /// </summary>
        /// <param name="curvePointDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///     "Id": (auto generated),
        ///     "Term": 1,
        ///     "Value": 2,
        ///     "CurveId": 1
        ///     "AsOfDate": "2021-01-01T00:00:00"
        ///     "CreatedDate": "2021-01-01T00:00:00"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<CurvePointDTO>> PostCurvePoint(CurvePointDTO curvePointDto)
        {
            if (_dbContext.CurvePoints == null)
            {
                return Problem("Entity set 'ApiDbContext.CurvePoints'  is null.");
            }

            var newCurvePoint = new CurvePoint()
            {
                Term = curvePointDto.Term == 0 ? 0 : curvePointDto.Term,
                Value = curvePointDto.Value == 0 ? 0 : curvePointDto.Value,
                CurveId = curvePointDto.CurveId == 0 ? 0 : curvePointDto.CurveId,
                AsOfDate = curvePointDto.AsOfDate == DateTime.MinValue ? DateTime.MinValue : curvePointDto.AsOfDate,
                CreationDate = curvePointDto.CreationDate == DateTime.MinValue ? DateTime.MinValue : curvePointDto.CreationDate
            };
            
            _dbContext.CurvePoints.Add(newCurvePoint);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCurvePoint", new { id = curvePointDto.Id }, curvePointDto);
        }

        // DELETE: api/CurvePoint/5
        /// <summary>
        /// Delete a specific CurvePoint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(long id)
        {
            if (_dbContext.CurvePoints == null)
            {
                return NotFound();
            }

            var curvePoint = await _dbContext.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            _dbContext.CurvePoints.Remove(curvePoint);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CurvePointExists(long id)
        {
            return (_dbContext.CurvePoints?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static CurvePointDTO CurvePointToDTO(CurvePoint curvePoint) =>
            new CurvePointDTO()
            {
                Id = curvePoint.Id,
                CurveId = curvePoint.CurveId,
                AsOfDate = curvePoint.AsOfDate,
                Term = curvePoint.Term,
                Value = curvePoint.Value,
                CreationDate = curvePoint.CreationDate
            };
    }
}