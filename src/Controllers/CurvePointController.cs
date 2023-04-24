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
    public class CurvePointController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public CurvePointController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/CurvePoint
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

            curvePoint.Term = curvePointDto.Term;
            //TODO: to complete

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurvePointDTO>> PostCurvePoint(CurvePointDTO curvePointDto)
        {
          if (_dbContext.CurvePoints == null)
          {
              return Problem("Entity set 'ApiDbContext.CurvePoints'  is null.");
          }
          
          var newCurvePoint = new CurvePoint()
          {
              Term = curvePointDto.Term,
              //TODO: to complete
          };
            _dbContext.CurvePoints.Add(newCurvePoint);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCurvePoint", new { id = curvePointDto.Id }, curvePointDto);
        }

        // DELETE: api/CurvePoint/5
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
